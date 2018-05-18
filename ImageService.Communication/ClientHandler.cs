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
    public class ClientHandler : IClientCommunicationChannel {
        private TcpClient m_client;
        private NetworkStream m_stream;
        private StreamReader m_reader;
        private StreamWriter m_writer;

        public event EventHandler<DataRecievedEventArgs> OnDataRecieved;
        public event EventHandler<EventArgs> OnClosed;

        public ClientHandler(TcpClient client) {
            m_client = client;
            m_stream = client.GetStream();
            m_reader = new StreamReader(m_stream, Encoding.ASCII);
            m_writer = new StreamWriter(m_stream, Encoding.ASCII);
        }

        public void Close() {
            m_reader.Close();
            m_writer.Close();
            m_stream.Close();
            m_client.Close();
            OnClosed?.Invoke(this, new EventArgs());
        }
        public bool Start() {
            try {
                new Task(() => {
                    string str;
                    while(true) {
                        str = m_reader.ReadLine();

                        if(str != null)
                            OnDataRecieved?.Invoke(this, new DataRecievedEventArgs(str));
                    }
                }).Start();
                return true;
            } catch {
                Close();
                return false;
            }
        }

        public void Send(object sender, DataRecievedEventArgs e) {
            Send(e.Data);
        }

        public int Send(string data) {
            try {
                m_writer.WriteLine(data);
                m_writer.Flush();
                return 1;
            } catch {
                this.Close();
                return 0;
            }
        }
    }
}
