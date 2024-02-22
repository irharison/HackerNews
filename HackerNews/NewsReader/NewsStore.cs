using HackerNews.ApiDto;
using HackerNews.Interfaces;


namespace HackerNews.NewsReader
{
    public class ArticleStore : INewsStore 
    {
        private Dictionary<int, NewsArticle> Articles = new Dictionary<int, NewsArticle>();

        public List<NewsArticle> GetArticles(int articleCount)
        {
            return Articles.OrderByDescending(x => x.Value.Score).Select(x=>x.Value).Take(articleCount).ToList();
        }

        public void AddArticle(int id, NewsArticle article)
        {
            if (!Articles.ContainsKey(id))
            {
                Articles.Add(id, article);
            }
        }

        public List<int> GetMissingArticles(List<int> newArticleList)
        {
            var missingAricles = newArticleList.Where(newArticle => !Articles.ContainsKey(newArticle)).ToList();
            return missingAricles;
        }
    }
}
