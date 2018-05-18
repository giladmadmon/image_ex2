using ImageService.Communication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Communication.Model.Event {
    public class DataRecievedEventArgs : EventArgs {
        public string Data { get; private set; }

        public DataRecievedEventArgs(string data) {
            this.Data = data;
        }
    }
}
