namespace wordleboard.Web
{
    public class AppUtils
    {

        public static int TodayWordleId => DateTime.Now.Subtract(new DateTime(2021, 6, 19)).Days;

        public static string FormatDate(long seconds)
        {
            return GetDateFromSeconds(seconds).ToString("dd MMM yyyy");
        }

        public static DateTime GetDateFromSeconds(long seconds)
        {
            DateTime date = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            date = date.AddSeconds(seconds).ToLocalTime();
            return date;
        }

        public static int GetWordleDayFromTimestamp(long seconds)
        {
            var date = GetDateFromSeconds(seconds);

            var wordleId = date.Subtract(new DateTime(2021, 6, 19)).Days;

            return wordleId;
        }

        public static string GetDateFromWordleId(int wordleId)
        {
            var formattedDate = (new DateTime(2021, 6, 19).AddDays(wordleId)).ToString("d MMMM yyyy");
            return formattedDate;
        }

    }
}
