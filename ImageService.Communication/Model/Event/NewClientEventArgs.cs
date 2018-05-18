using ImageService.Communication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Communication.Model.Event {
    public class NewClientEventArgs : EventArgs {
        public IClientCommunicationChannel NewClient { get; set; }
        public NewClientEventArgs(IClientCommunicationChannel newClient) {
            NewClient = newClient;
        }
    }
}
