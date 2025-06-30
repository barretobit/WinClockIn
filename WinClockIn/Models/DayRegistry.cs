namespace WinClockIn.Models
{
    public class DayRegistry
    {
        public DateTime LogIn { get; set; }
        public DateTime LunchBreakIn { get; set; }
        public DateTime LunchBreakOff { get; set; }
        public DateTime LogOff { get; set; }
        public int TotalTime { get; set; }
        public int LunchTime { get; set; }
        public int SmallBreaksTime { get; set; }
    }
}