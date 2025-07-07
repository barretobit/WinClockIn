using WinClockIn.Models;

namespace WinClockIn.Services
{
    public static class ClockService
    {
        public static void ClockIn(DayRegistry dayRegistry)
        {
            var now = DateTime.Now;
            var todayRegistry = DataService.GetTodayRegistry();

            if (todayRegistry == null)
            {
                // First clock-in of the day
                var newRegistry = new DayRegistry
                {
                    LogIn = now
                };

                DataService.SaveOrUpdateRegistry(newRegistry);
                LogService.LogThis("ClockIn", "LogIn recorded");
                return;
            }

            // If LogIn is already set, maybe the user is returning from lunch
            if (todayRegistry.LunchBreakOff == default && todayRegistry.LunchBreakIn != default)
            {
                todayRegistry.LunchBreakOff = now;
                DataService.SaveOrUpdateRegistry(todayRegistry);
                LogService.LogThis("ClockIn", "LunchBreakOff recorded");
                return;
            }

            // If both are already filled, we can optionally ignore or log it
            LogService.LogThis("ClockIn", "ClockIn ignored — already logged in and lunch break off set");
        }
    }
}