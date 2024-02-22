namespace HackerNews.ApiDto
{
    public class NewsArticle
    {
        public string Title { get; set; } = String.Empty;
        public string Url { get; set; } = String.Empty;
        public string PostedBy { get; set; } = String.Empty;
        public string Time { get; set; } = String.Empty;
        public int Score { get; set; }
        public int CommentCount { get; set; }
    }
}