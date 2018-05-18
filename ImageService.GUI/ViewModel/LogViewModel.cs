using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Logging.Modal;
using ImageService.Logging;
using System.ComponentModel;
using System.Collections.Specialized;
using ImageService.GUI.Model;

namespace ImageService.GUI.ViewModel {
    public class LogViewModel : INotifyPropertyChanged {
        private LogModel m_model;
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<LogMessageRecord> VM_LogMessages { get { return m_model.LogMessages; } }

        public LogViewModel(LogModel model) {
            m_model = model;
            m_model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e) {
                NotifyProperyChanged("VM_" + e.PropertyName);
            };
        }

        private void NotifyProperyChanged(string name) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

