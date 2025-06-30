namespace WinClockIn.Services
{
    public static class StateService
    {
        public static bool IsLoggedIn(DateTime dateTimeNow)
        {
            var all = FileService.LoadAll();
            var today = all.FirstOrDefault(x => x.LogIn.Date == dateTimeNow.Date);
            return today != null && today.LogIn != default;
        }
    }
}