using System.IO;
using System.Text.Json;
using WinClockIn.Models;

namespace WinClockIn.Services
{
    public static class FileService
    {
        private static readonly string FolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
        private static readonly string FilePath = Path.Combine(FolderPath, "registry.json");

        static FileService()
        {
            if (!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);

            if (!File.Exists(FilePath))
                File.WriteAllText(FilePath, "[]"); // Create empty array to start
        }

        public static string ReadFile()
        {
            return File.ReadAllText(FilePath);
        } 

        public static void UpdateFile(string entireFile)
        {
            File.WriteAllText(FilePath, entireFile);
        }
    }
}