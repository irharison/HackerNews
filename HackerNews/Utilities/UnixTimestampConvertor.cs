namespace HackerNews.Utilities
{
    public class UnixTimeStampConvertor
    {

        public static string UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();

            return dateTime.ToString(("yyyy-MM-ddTHH:mm:ssK"));
        }
    }
}
