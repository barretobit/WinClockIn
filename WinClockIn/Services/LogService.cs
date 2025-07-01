using System.IO;

namespace WinClockIn.Services
{
    public static class LogService
    {
        private static readonly object _lock = new();
        private static readonly string LogDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
        private static readonly string LogFilePath = Path.Combine(LogDirectory, $"log_{DateTime.Now:yyyyMMdd}.txt");

        static LogService()
        {
            if (!Directory.Exists(LogDirectory))
                Directory.CreateDirectory(LogDirectory);
        }

        public static void LogThis(string action, string error)
        {
            var logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ACTION: {action} | ERROR: {error}";

            lock (_lock)
            {
                File.AppendAllText(LogFilePath, logEntry + Environment.NewLine);
            }
        }
    }
}