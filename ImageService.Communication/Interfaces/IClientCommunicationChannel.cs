using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Communication.Interfaces {
    public interface IClientCommunicationChannel : ICommunicationChannel {
        int Send(string data);
    }
}
