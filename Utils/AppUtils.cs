namespace wordleboard.Utils
{
    public class AppUtils
    {
        public static int TodayWordleId => DateTime.Now.Subtract(AppConstants.WordleStartDate).Days;
    }
}
