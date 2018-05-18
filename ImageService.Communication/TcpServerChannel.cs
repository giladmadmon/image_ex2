using ImageService.Communication.Interfaces;
using ImageService.Communication.Model.Event;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Communication {
    public class TcpServerChannel : IClientCommunicationChannel {
        private string m_ip;
        private int m_port;
        private TcpListener m_listener;

        public event EventHandler<DataRecievedEventArgs> OnDataSending;
        public event EventHandler<DataRecievedEventArgs> OnDataRecieved;

        public TcpServerChannel(string ip, int port) {
            this.m_ip = ip;
            this.m_port = port;
        }
        public void Close() {
            throw new NotImplementedException();
        }

        public bool Start() {
            try {
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse(m_ip), m_port);
                m_listener = new TcpListener(ep);
                m_listener.Start();
                Console.WriteLine("Waiting for client connections...");
                new Task(() => {
                    while(true) {
                        TcpClient client = m_listener.AcceptTcpClient();
                        ClientHandler handler = new ClientHandler(client);
                        OnDataSending += handler.Send;
                        handler.OnDataRecieved += this.OnDataRecieved;
                        handler.OnClosed += (sender, e) => OnDataSending -= handler.Send;
                        handler.Start();
                    }
                }).Start();
                return true;
            } catch {
                return false;
            }
        }

        private void TcpServerChannel_OnDataSending(object sender, DataRecievedEventArgs e) {
            throw new NotImplementedException();
        }

        public int Send(string data) {
            try {
                OnDataSending?.Invoke(this, new DataRecievedEventArgs(data));
                return 1;
            } catch {
                return 0;
            }
        }
    }
}

