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

        public static void SaveOrUpdateRegistry(DayRegistry registry)
        {
            var all = LoadAll();

            // Ensure only one entry per day (based on LogIn date)
            var existing = all.FirstOrDefault(x => x.LogIn.Date == registry.LogIn.Date);
            if (existing != null)
            {
                all.Remove(existing);
            }

            all.Add(registry);

            var json = JsonSerializer.Serialize(all, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }

        public static List<DayRegistry> LoadAll()
        {
            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<DayRegistry>>(json) ?? new List<DayRegistry>();
        }
    }
}