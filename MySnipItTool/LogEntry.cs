using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySnipItTool
{
    public class LogEntry
    {
        public LogEntry(string error, string stackTrace, string dateTime)
        {
            Error = error;
            StackTrace = stackTrace;
            DateTime = dateTime;
        }

        public string Error { get; set; }
        public string StackTrace { get; set; }
        public string DateTime { get; set; }

        public string[] ToStringArray()
        {
            string[] output = new string[3];
            output[0] = this.DateTime;
            output[1] = this.Error;
            output[2] = this.StackTrace;
            return output;
        }
    }
}
