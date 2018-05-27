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

        private IClientCommunicationChannel m_client;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static ClientCommunication Instance {
            get {
                if(m_ClientCommunication == null) {
                    m_ClientCommunication = new ClientCommunication();
                }

                return m_ClientCommunication;
            }
        }
        /// <summary>
        /// Gets a value indicating whether this <see cref="ClientCommunication"/> is connected.
        /// </summary>
        /// <value>
        ///   <c>true</c> if connected; otherwise, <c>false</c>.
        /// </value>
        public bool Connected {
            get { return m_client.Connected; }
        }


        /// <summary>
        /// Occurs when the client receives data.
        /// </summary>
        public event EventHandler<DataReceivedEventArgs> OnDataRecieved {
            add { m_client.OnDataRecieved += value; }
            remove { m_client.OnDataRecieved -= value; }
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="ClientCommunication"/> class from being created.
        /// </summary>
        private ClientCommunication() {
            m_client = new TcpClientChannel(IP, PORT);
            m_client.Start();
        }

        /// <summary>
        /// Sends the specified command message.
        /// </summary>
        /// <param name="cmdMsg">The command message.</param>
        /// <returns></returns>
        public int Send(CommandMessage cmdMsg) {
            return m_client.Send(cmdMsg.ToJSON());
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public void Close() {
            Send(new CommandMessage(CommandEnum.CloseClientCommand, new string[] { }));
            m_client.Close();
            m_client = null;
        }
    }
}
