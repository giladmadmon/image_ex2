using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Communication.Interfaces {
    public interface IClientCommunicationChannel : ICommunicationChannel {
        /// <summary>
        /// Sends the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        int Send(string data);
    }
}
