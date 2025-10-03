using System;
using System.IO;

namespace Logger
{
    public class FileLogger : BaseLogger
    {
        private readonly string _filePath;

        public FileLogger(string filePath) 
        {
            _filePath = filePath;
        }
        public override void Log(LogLevel logLevel, string message)
        {
            string logLine = $"{DateTime.Now} {ClassName} {logLevel}: {message}";
            File.AppendAllText(_filePath, logLine + Environment.NewLine);
        }

    }
}
