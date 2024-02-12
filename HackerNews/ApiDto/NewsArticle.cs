namespace HackerNews.ApiDto
{
    public class NewsArticle
    {
        public string title { get; set; } = String.Empty;
        public string uri { get; set; } = String.Empty;
        public string postedBy { get; set; } = String.Empty;
        public string time { get; set; } = String.Empty;
        public int score { get; set; }
        public int commentCount { get; set; }
    }
}