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
    public class TcpClientChannel : ICommunicationChannel {
        private TcpClient m_client;
        private NetworkStream m_stream;
        private StreamReader m_reader;
        private StreamWriter m_writer;

        public event EventHandler<DataRecievedEventArgs> OnDataRecieved;
        public TcpClientChannel(TcpClient client) {
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
        }

        public bool Start() {
            new Task(() => {
                string str;
                try {
                    while(true) {
                        str = m_reader.ReadLine();
                        if(str != null) {
                            OnDataRecieved?.Invoke(this, new DataRecievedEventArgs(str));
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
                m_writer.WriteLine(data);
                return 1;
            } catch {
                return 0;
            }
        }
    }
}
