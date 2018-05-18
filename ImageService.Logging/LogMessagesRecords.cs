using ImageService.Logging;
using ImageService.Logging.Modal;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Logging {
    public class LogMessageRecord {
        public string Message { get; set; }
        public MessageTypeEnum Type { get; set; }

        public LogMessageRecord(string message, MessageTypeEnum type) {
            this.Message = message;
            this.Type = type;
        }

        public string ToJSON() {
            JObject logMessage = new JObject();

            logMessage["Message"] = this.Message;
            logMessage["Type"] = (int)this.Type;

            return logMessage.ToString();
        }

        public static LogMessageRecord FromJSON(string str) {
            JObject logMsgObj = JObject.Parse(str);

            LogMessageRecord logMsgRcrd = new LogMessageRecord(
            (string)logMsgObj["Message"],
            (MessageTypeEnum)(int)logMsgObj["Type"]);

            return logMsgRcrd;
        }
    }
}

public class LogMessageRecords : ObservableCollection<LogMessageRecord> {

    public LogMessageRecords() { }
    public LogMessageRecords(LogMessageRecords logMsgRcrds) : base(logMsgRcrds) { }

    public string ToJSON() {
        JObject logMessageRecords = new JObject();
        logMessageRecords["Size"] = this.Count;

        for(int i = 1; i <= this.Count; ++i) {
            JObject logMessage = new JObject();
            logMessage["Message"] = this[i - 1].Message;
            logMessage["Type"] = (int)this[i - 1].Type;
            logMessageRecords[i] = logMessage;
        }

        return logMessageRecords.ToString();
    }

    public static LogMessageRecords FromJSON(string str) {
        LogMessageRecords logMsgs = new LogMessageRecords();
        JObject logMsgsObj = JObject.Parse(str);
        int size = (int)logMsgsObj["Size"];

        for(int i = 1; i <= size; ++i) {
            LogMessageRecord logMsgRcrd = new LogMessageRecord(
            (string)logMsgsObj[i]["Message"],
            (MessageTypeEnum)(int)logMsgsObj[i]["Type"]
                );
        }

        return logMsgs;
    }
}
