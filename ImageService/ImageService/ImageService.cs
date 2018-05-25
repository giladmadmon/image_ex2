using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Configuration;
using ImageService.Server;
using ImageService.Controller;
using ImageService.Modal;
using ImageService.Logging;
using System.Security.Permissions;
using ImageService.ImageService.Modal;
using ImageService.Communication.Model;
using ImageService.Communication.Interfaces;
using ImageService.Infrastructure.Enums;
using ImageService.ImageService.Server;

public enum ServiceState {
    SERVICE_STOPPED = 0x00000001,
    SERVICE_START_PENDING = 0x00000002,
    SERVICE_STOP_PENDING = 0x00000003,
    SERVICE_RUNNING = 0x00000004,
    SERVICE_CONTINUE_PENDING = 0x00000005,
    SERVICE_PAUSE_PENDING = 0x00000006,
    SERVICE_PAUSED = 0x00000007,
}

[StructLayout(LayoutKind.Sequential)]
public struct ServiceStatus {
    public int dwServiceType;
    public ServiceState dwCurrentState;
    public int dwControlsAccepted;
    public int dwWin32ExitCode;
    public int dwServiceSpecificExitCode;
    public int dwCheckPoint;
    public int dwWaitHint;
};

namespace ImageService {
    public partial class x : ServiceBase {

        private ImageServer m_imageServer;
        private IImageController m_controller;

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public x() {
            InitializeComponent();

            string sourceName =AppConfig.Instance.SourceName;
            string logName = AppConfig.Instance.LogName;

            if(!EventLog.SourceExists(sourceName)) {
                EventLog.CreateEventSource(sourceName, logName);
            }

            this.EventLogger.Source = sourceName;
        }

        protected override void OnStart(string[] args) {
            string outptDir = AppConfig.Instance.OutputDirPath;
            int thumbnailSize = Int32.Parse(AppConfig.Instance.ThumbnailSize);
            List<string> handlers = AppConfig.Instance.Folders;

            IImageServiceModal modal = new ImageServiceModal(outptDir, thumbnailSize);

            ILoggingService logging = new LoggingService();
            logging.MessageRecieved += UpdateLogInServer;

            m_controller = new ImageController(modal, logging);
            this.m_imageServer = new ImageServer(m_controller, logging);

            logging.MessageRecieved += (sender, msgReceived) => {
                this.EventLogger.WriteEntry(msgReceived.Message, (EventLogEntryType)msgReceived.Status);
            };

            foreach(string handler in handlers) {
                this.m_imageServer.createHandler(handler);
            }

            ServerCommunication.Instance.OnDataReceived += OnServerDataRecieved;

            this.EventLogger.WriteEntry("Service Started");
        }

        protected override void OnStop() {
            m_imageServer.CloseServer();
            this.EventLogger.WriteEntry("Service Stopped");
        }

        private void EventLogger_EntryWritten(object sender, EntryWrittenEventArgs e) {

        }

        #region ServerCommunication events
        private void OnServerDataRecieved(object sender, Communication.Model.Event.DataReceivedEventArgs e) {
            CommandMessage cmdMsg = CommandMessage.FromJSON(e.Data);
            IClientCommunicationChannel receiver = (IClientCommunicationChannel)sender;
            string msg = null;

            if(cmdMsg.CmdId == CommandEnum.CloseClientCommand) {
                receiver.Close();
            } else if(cmdMsg.CmdId == CommandEnum.CloseCommand) {
                m_imageServer.sendCommand(CommandEnum.CloseCommand, new string[] { }, cmdMsg.Args[0]);
                ServerCommunication.Instance.Send(new CommandMessage(CommandEnum.CloseCommand, cmdMsg.Args).ToJSON());
            } else {
                bool result;
                msg = m_controller.ExecuteCommand((int)cmdMsg.CmdId, cmdMsg.Args, out result);
            }

            if(msg != null) {
                receiver.Send(msg);
            }
        }

        private void UpdateLogInServer(object sender, Logging.Modal.MessageRecievedEventArgs e) {
            LogMessageRecord record = new LogMessageRecord(e.Message, e.Status);
            CommandMessage cmd = new CommandMessage(CommandEnum.LogCommand, new string[] { record.ToJSON() });
            ServerCommunication.Instance.Send(cmd.ToJSON());
        }

        #endregion

    }
}
