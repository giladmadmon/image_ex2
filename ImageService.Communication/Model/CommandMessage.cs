using ImageService.Infrastructure.Enums;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Communication.Model {
    public class CommandMessage {

        public CommandEnum CmdId { get; private set; }
        public string[] Args { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandMessage"/> class.
        /// </summary>
        /// <param name="cmdId">The command identifier.</param>
        /// <param name="args">The arguments.</param>
        public CommandMessage(CommandEnum cmdId, string[] args) {
            this.CmdId = cmdId;
            this.Args = args;

        }
        public string ToJSON() {
            JObject commandMessage = new JObject();
            commandMessage["CmdId"] = (int)this.CmdId;
            for(int i = 1; i <= this.Args.Length; ++i) {
                commandMessage["Args"][i] = (string)this.Args[i - 1];
            }
            commandMessage["ArgsNum"] = this.Args.Length;

            return commandMessage.ToString();
        }

        public static CommandMessage FromJSON(string str) {

            JObject commandMessage = JObject.Parse(str);
            int size = (int)commandMessage["ArgsNum"];
            int cmdId = (int)commandMessage["CmdId"];
            string[] args = new string[size];
            for(int i = 1; i <= size; ++i) {
               args[i-1] = (string)commandMessage["Args"][i];
            }

            return new CommandMessage((CommandEnum)cmdId,args);
        }
    }
}
