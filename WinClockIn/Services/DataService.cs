using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WinClockIn.Models;

namespace WinClockIn.Services
{
    public static class DataService
    {
        public static List<DayRegistry> LoadAll()
        {
            string fullFile = FileService.ReadFile();
            return JsonSerializer.Deserialize<List<DayRegistry>>(fullFile) ?? new List<DayRegistry>();
        }

        public static DayRegistry? GetTodayRegistry()
        {
            DateTime today = DateTime.Today;

            List<DayRegistry> allRegistries = LoadAll();

            DayRegistry? todayRegistry = allRegistries.FirstOrDefault(r => r.LogIn.Date == today.Date);

            return todayRegistry;
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

            string entireFile = JsonSerializer.Serialize(all, new JsonSerializerOptions { WriteIndented = true });
            
            FileService.UpdateFile(entireFile);
        }
    }
}
