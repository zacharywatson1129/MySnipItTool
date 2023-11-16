using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MySnipItTool
{
    public static class LogUtil
    {
        public static void LogToFile(string fileName, string[] linesToWrite)
        {
            File.WriteAllLines(fileName, linesToWrite);
        }
    }
}
