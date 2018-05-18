using ImageService.Logging;
using ImageService.Logging.Modal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.GUI.Model {
    public class LogModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        public LogMessageRecords LogMessages { set; get; }

        public LogModel() {
            LogMessages = new LogMessageRecords();
            //////////delete afterrrrrrr
            LogMessages.Add(new LogMessageRecord("FAIL", MessageTypeEnum.FAIL));
            LogMessages.Add(new LogMessageRecord("WARN", MessageTypeEnum.WARNING));
            LogMessages.Add(new LogMessageRecord("INFO", MessageTypeEnum.INFO));

            LogMessages.CollectionChanged += delegate (object sender, NotifyCollectionChangedEventArgs e) {
                NotifyProperyChanged("LogMessages");
            };
        }
        private void NotifyProperyChanged(string name) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
