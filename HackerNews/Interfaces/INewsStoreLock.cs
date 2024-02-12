using HackerNews.ApiDto;

namespace HackerNews.Interfaces
{
    public interface INewsStoreLock
    {
        List<NewsArticle> GetArticles(int articleCount);

        void UpdateArticleList(Dictionary<int, NewsArticle> newArticles);

        List<int> GetMissingArticles(List<int> newArticleList);
    }
}
