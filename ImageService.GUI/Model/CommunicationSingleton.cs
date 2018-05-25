using ImageService.Communication;
using ImageService.Communication.Interfaces;
using ImageService.Communication.Model;
using ImageService.Communication.Model.Event;
using ImageService.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.GUI.Model {
    public class ClientCommunication {
        private static string IP = "127.0.0.1";
        private static int PORT = 8003;

        private static ClientCommunication m_ClientCommunication = null;

        public static ClientCommunication Instance {
            get {
                if(m_ClientCommunication == null) {
                    m_ClientCommunication = new ClientCommunication();
                }

                return m_ClientCommunication;
            }
        }

        private IClientCommunicationChannel m_client;
        public event EventHandler<DataReceivedEventArgs> OnDataRecieved {
            add { m_client.OnDataRecieved += value; }
            remove { m_client.OnDataRecieved -= value; }
        }

        private ClientCommunication() {
            m_client = new TcpClientChannel(IP, PORT);
            m_client.Start();
        }

        public int Send(CommandMessage cmdMsg) {
            return m_client.Send(cmdMsg.ToJSON());
        }

        public void Close() {
            Send(new CommandMessage(CommandEnum.CloseClientCommand, new string[] { }));
            m_client.Close();
            m_client = null;
        }
    }
}
