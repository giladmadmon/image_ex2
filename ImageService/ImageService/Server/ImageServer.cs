﻿using ImageService.Controller;
using ImageService.Controller.Handlers;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Commands;
using System.IO;
using ImageService.Communication.Interfaces;
using ImageService.Communication;
using ImageService.Communication.Model;

namespace ImageService.Server {
    public class ImageServer {

        #region Members
        private const string IP = "127.0.0.1";
        private const int PORT = 8003;
        private IClientCommunicationChannel m_server;
        private IImageController m_controller;
        private ILoggingService m_logging;
        #endregion

        #region Properties
        public event EventHandler<CommandRecievedEventArgs> CommandRecieved;          // The event that notifies about a new Command being recieved
        #endregion

        /// <summary>
        /// create new image server
        /// </summary>
        /// <param name="controller"> the controller used by the server and its handlers </param>
        /// <param name="logging"> the logger used by the server and its handlers </param>
        public ImageServer(IImageController controller, ILoggingService logging) {
            this.m_controller = controller;
            this.m_logging = logging;
            m_server = new TcpServerChannel(IP, PORT);
            m_server.OnDataRecieved += OnServerDataRecieved;
            m_logging.MessageRecieved += UpdateLogInServer;
        }

        private void OnServerDataRecieved(object sender, Communication.Model.Event.DataRecievedEventArgs e) {
            CommandMessage cmdMsg = CommandMessage.FromJSON(e.Data);
            IClientCommunicationChannel receiver = (IClientCommunicationChannel)sender;

            if(cmdMsg.CmdId == CommandEnum.CloseClientCommand) {
                receiver.Close();
            } else {
                bool result;
                string msg = m_controller.ExecuteCommand((int)cmdMsg.CmdId, cmdMsg.Args, out result);
                if(msg != null) {
                    receiver.Send(msg);
                }
            }
        }

        private void UpdateLogInServer(object sender, Logging.Modal.MessageRecievedEventArgs e) {
            LogMessageRecord record = new LogMessageRecord(e.Message, e.Status);
            CommandMessage cmd = new CommandMessage(CommandEnum.LogCommand, new string[] { record.ToJSON() });
            m_server.Send(cmd.ToJSON());
        }

        /// <summary>
        /// creating a new handler.
        /// </summary>
        /// <param name="directory"> the directory the handler listens to </param>
        public void createHandler(string directory) {
            if(Directory.Exists(directory)) {
                IDirectoryHandler dirHandler = new DirectoyHandler(m_controller, m_logging);

                CommandRecieved += dirHandler.OnCommandRecieved;
                dirHandler.DirectoryClose += CloseHandler;

                dirHandler.StartHandleDirectory(directory.Trim());
            } else {
                m_logging.Log("Directory \"" + directory + "\" does not exist!", Logging.Modal.MessageTypeEnum.FAIL);
            }
        }
        /// <summary>
        /// send command to all the handlers.
        /// </summary>
        /// <param name="commandId"> the command required to be performed </param>
        /// <param name="args"> the arguments for the command </param>
        /// <param name="path"> the path related to the command </param>
        public void sendCommand(CommandEnum commandId, string[] args, string path) {
            CommandRecievedEventArgs cmdEventArgs = new CommandRecievedEventArgs((int)commandId, args, path);
            CommandRecieved(this, cmdEventArgs);
            m_logging.Log(path + " - " + commandId, Logging.Modal.MessageTypeEnum.INFO);
        }
        /// <summary>
        /// close specific handler
        /// </summary>
        /// <param name="sender"> the object calling to this function </param>
        /// <param name="eventArgs"> the event args of closing a handler </param>
        private void CloseHandler(object sender, DirectoryCloseEventArgs eventArgs) {
            IDirectoryHandler dirHandler = (IDirectoryHandler)sender;
            CommandRecieved -= dirHandler.OnCommandRecieved;
            dirHandler.DirectoryClose -= CloseHandler;
        }
        /// <summary>
        /// close all the handlers of the server
        /// </summary>
        public void CloseServer() {
            sendCommand(CommandEnum.CloseCommand, new string[] { }, "");
        }
    }
}
