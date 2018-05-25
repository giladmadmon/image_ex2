using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Communication.Model.Event {
    public class DataReceivedEventArgs : EventArgs {
        public string Data { get; private set; }

        public DataReceivedEventArgs(string data) {
            this.Data = data;
        }
    }
}
