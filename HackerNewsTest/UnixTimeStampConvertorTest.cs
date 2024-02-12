using Xunit;
using HackerNews.Utilities;

namespace HackerNewsTest
{
    public class UnixTimeStampConvertorTest
    {
        [Theory]
        [InlineData( 1, "1970-01-01T00:00:01+00:00")]
        [InlineData(1707493595, "2024-02-09T15:46:35+00:00")]
        public void TestConversion(int date, string returnDate)
        {
            string convertedDate = UnixTimeStampConvertor.UnixTimeStampToDateTime(date);
            Assert.Equal(returnDate, convertedDate);
        }
    }
}
