using ImageService.GUI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.GUI.ViewModel {
    class SettingsViewModel : INotifyPropertyChanged {
        private ISettingsModel m_model;

        public string VM_OutputDirPath { get { return m_model.OutputDirPath; } }
        public string VM_SourceName { get { return m_model.SourceName; } }
        public string VM_LogName { get { return m_model.LogName; } }
        public string VM_ThumbnailSize { get { return m_model.ThumbnailSize; } }
        public ObservableCollection<string> VM_Folders { get { return m_model.Folders; } }

        public event PropertyChangedEventHandler PropertyChanged;

        public SettingsViewModel(ISettingsModel model) {
            m_model = model;
            m_model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyProperyChanged("VM_" + e.PropertyName);
            };
            //////////////delete
            new Task(() => {
                m_model.Folders.Add("and");
                m_model.Folders.Add("super");
                m_model.Folders.Add("gonna");
                m_model.Folders.Add("stay");

                m_model.OutputDirPath = "dafna";
                m_model.SourceName = "is";
                m_model.LogName = "very";
                m_model.ThumbnailSize = "here";

            }).Start();
        }

        private void NotifyProperyChanged(string name) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
