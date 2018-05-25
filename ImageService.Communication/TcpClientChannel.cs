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
    public class TcpClientChannel : IClientCommunicationChannel {
        private TcpClient m_client;
        private NetworkStream m_stream;
        private BinaryReader m_reader;
        private BinaryWriter m_writer;

        public event EventHandler<DataReceivedEventArgs> OnDataRecieved;
        public TcpClientChannel(string ip, int port) {
            m_client = new TcpClient();
            m_client.Connect(ip, port);
            InitClient();
        }
        public TcpClientChannel(TcpClient client) {
            m_client = client;
            InitClient();
        }

        private void InitClient() {
            m_stream = m_client.GetStream();
            m_reader = new BinaryReader(m_stream);
            m_writer = new BinaryWriter(m_stream);
        }

        public void Close() {
            m_reader.Close();
            m_writer.Close();
            m_stream.Close();
            m_client.Close();
        }

        public bool Start() {
            new Task(() => {
                char[] buffer = new char[1024];
                try {
                    while(true) {
                        string data = m_reader.ReadString();
                        if(data.ToString() != "") {
                            OnDataRecieved?.Invoke(this, new DataReceivedEventArgs(data.ToString()));
                        }
                    }
                } catch {
                    Close();
                }
            }).Start();
            return true;
        }

        public int Send(string data) {
            try {
                m_writer.Write(data.Trim());
                m_writer.Flush();
                return 1;
            } catch {
                Close();
                return 0;
            }
        }
    }
}
