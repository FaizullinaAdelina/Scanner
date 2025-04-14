using System;
using System.IO;

namespace Scanner
{
    public static class Logger
    {
        private static readonly object lockObj = new object();
        // Логи будут сохраняться в папке "logs" внутри корня приложения
        private static readonly string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs", "error.log");

        static Logger()
        {
            // Создаём папку, если её нет
            var logDir = Path.GetDirectoryName(logFilePath);
            if (!Directory.Exists(logDir))
            {
                Directory.CreateDirectory(logDir);
            }
        }

        public static void LogError(string message)
        {
            lock (lockObj)
            {
                File.AppendAllText(logFilePath, $"[{DateTime.Now}] ERROR: {message}{Environment.NewLine}");
            }
        }

        public static void LogInfo(string message)
        {
            lock (lockObj)
            {
                File.AppendAllText(logFilePath, $"[{DateTime.Now}] INFO: {message}{Environment.NewLine}");
            }
        }
    }
}
