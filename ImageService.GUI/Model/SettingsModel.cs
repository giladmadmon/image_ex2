using ImageService.Communication.Model;
using ImageService.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ImageService.GUI.Model {
    class SettingsModel : ISettingsModel {
        private string m_outputDirPath;
        private string m_sourceName;
        private string m_logName;
        private string m_thumbnailSize;
        private ObservableCollection<string> m_Folders;

        public event PropertyChangedEventHandler PropertyChanged;

        public SettingsModel() {
            Folders = new ObservableCollection<string>();
            BindingOperations.EnableCollectionSynchronization(Folders, new object());
            Folders.CollectionChanged += (sender, e) => NotifyProperyChanged("Folders");

            ClientCommunication.Instance.OnDataRecieved += Instance_OnDataRecieved;
            ClientCommunication.Instance.OnDataRecieved += Instance_OnDataRecieved1;
            ;
            ClientCommunication.Instance.Send(new CommandMessage(CommandEnum.GetConfigCommand, new string[] { }));
        }

        private void Instance_OnDataRecieved1(object sender, Communication.Model.Event.DataReceivedEventArgs e) {
            CommandMessage cmdMsg = CommandMessage.FromJSON(e.Data);
            if(cmdMsg.CmdId == CommandEnum.CloseCommand) {
                Folders.Remove(cmdMsg.Args[0]);
            }
        }

        private void Instance_OnDataRecieved(object sender, Communication.Model.Event.DataReceivedEventArgs e) {
            CommandMessage cmdMsg = CommandMessage.FromJSON(e.Data);
            if(cmdMsg.CmdId == CommandEnum.GetConfigCommand) {
                this.SourceName = cmdMsg.Args[0];
                this.LogName = cmdMsg.Args[1];
                this.OutputDirPath = cmdMsg.Args[2];
                this.ThumbnailSize = cmdMsg.Args[3];
                foreach(string folder in cmdMsg.Args[4].Trim().Split(';')) {
                    if(!folder.Equals(""))
                        Folders.Add(folder);
                }
            }
        }

        public string LogName {
            get {
                return m_logName;
            }

            set {
                if(value != m_logName) {
                    m_logName = value;
                    NotifyProperyChanged("LogName");
                }
            }
        }
        public string SourceName {
            get {
                return m_sourceName;
            }

            set {
                if(value != m_sourceName) {
                    m_sourceName = value;
                    NotifyProperyChanged("SourceName");
                }
            }
        }
        public string OutputDirPath {
            get {
                return m_outputDirPath;
            }

            set {
                if(value != m_outputDirPath) {
                    m_outputDirPath = value;
                    NotifyProperyChanged("OutputDirPath");
                }
            }
        }
        public string ThumbnailSize {
            get {
                return m_thumbnailSize;
            }

            set {
                if(value != m_thumbnailSize) {
                    m_thumbnailSize = value;
                    NotifyProperyChanged("ThumbnailSize");
                }
            }
        }

        public void ResetSettings() {
            ////////internet
            throw new NotImplementedException();
        }

        private void NotifyProperyChanged(string name) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public ObservableCollection<string> Folders {
            get {
                return m_Folders;
            }
            set {
                if(value != m_Folders) {
                    m_Folders = value;
                    NotifyProperyChanged("Folders");
                }
            }
        }
    }
}
