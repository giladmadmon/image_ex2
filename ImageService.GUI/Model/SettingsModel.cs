using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.GUI.Model {
    class SettingsModel : ISettingsModel {
        private string m_outputDirPath;
        private string m_sourceName;
        private string m_logName;
        private string m_thumbnailSize;
        private ObservableCollection<string> m_Folders;

        public event PropertyChangedEventHandler PropertyChanged;

        public SettingsModel() {
            m_Folders = new ObservableCollection<string>();
            m_Folders.CollectionChanged += (sender, e) => NotifyProperyChanged("Folders");
        }

        private void M_Folders_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            throw new NotImplementedException();
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
