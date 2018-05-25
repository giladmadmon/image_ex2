using ImageService.Communication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Communication.Model.Event;
using System.Net.Sockets;
using System.IO;

namespace ImageService.Communication {
    public class ClientHandler : TcpClientChannel {
        public event EventHandler<EventArgs> OnClosed;

        public ClientHandler(TcpClient client) : base(client) { }

        public new void Close() {
            base.Close();
            OnClosed?.Invoke(this, new EventArgs());
        }

        public void Send(object sender, DataReceivedEventArgs e) {
            Send(e.Data);
        }
    }
}
