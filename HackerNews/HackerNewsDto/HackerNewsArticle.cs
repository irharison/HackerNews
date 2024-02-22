namespace HackerNews.HackerNewsDto
{
    public class HackerNewsArticle
    {
        public string By { get; set; }
        public int Descendents { get; set; }
        public int Id { get; set; }
        public int[] Kids { get; set; }
        public int Score { get; set; }
        public int Time { get; set; }
        public string Title { get; set; }
        public string Story { get; set; }
        public string Url { get; set; }
   }
}
