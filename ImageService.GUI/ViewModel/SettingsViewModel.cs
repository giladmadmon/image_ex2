using ImageService.Communication.Model;
using ImageService.GUI.Model;
using ImageService.Infrastructure.Enums;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ImageService.GUI.ViewModel {
    class SettingsViewModel : INotifyPropertyChanged {
        private ISettingsModel m_model;
        private string m_SelectedItem;

        public string VM_SelectedItem {
            get { return m_SelectedItem; }
            set { NotifyProperyChanged("SelectedItem"); m_SelectedItem = value; }
        }
        public string VM_OutputDirPath { get { return m_model.OutputDirPath; } }
        public string VM_SourceName { get { return m_model.SourceName; } }
        public string VM_LogName { get { return m_model.LogName; } }
        public string VM_ThumbnailSize { get { return m_model.ThumbnailSize; } }
        public ObservableCollection<string> VM_Folders { get { return m_model.Folders; } }

        public ICommand RemoveCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public SettingsViewModel(ISettingsModel model) {
            m_model = model;
            m_model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyProperyChanged("VM_" + e.PropertyName);
            };

            this.RemoveCommand = (ICommand)new DelegateCommand<object>(new Action<object>(this.OnSubmit), new Func<object, bool>(this.CanSubmit));
            this.PropertyChanged += new PropertyChangedEventHandler(this.PropertyChangedCheck);
        }

        private void OnSubmit(object obj) {
            ClientCommunication.Instance.Send(new CommandMessage(CommandEnum.CloseCommand, new string[] { VM_SelectedItem }));
        }

        private bool CanSubmit(object obj) {
            return VM_SelectedItem != null;
        }

        private void PropertyChangedCheck(object sender, PropertyChangedEventArgs e) {
            (this.RemoveCommand as DelegateCommand<object>).RaiseCanExecuteChanged();
        }

        private void NotifyProperyChanged(string name) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
