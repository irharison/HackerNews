using HackerNews.ApiDto;

namespace HackerNews.Interfaces
{
    public interface INewsStore
    {
        List<NewsArticle> GetArticles(int articleCount);
        void AddArticle(int id, NewsArticle article);
        List<int> GetMissingArticles(List<int> newArticleList);
    }
}
