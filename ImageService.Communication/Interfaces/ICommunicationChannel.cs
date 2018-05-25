using ImageService.Communication.Model.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Communication.Interfaces {
    public interface ICommunicationChannel {
        event EventHandler<DataReceivedEventArgs> OnDataRecieved;
        void Close();
        bool Start();
    }
}
