using HackerNews.HackerNewsDto;

namespace HackerNews.Interfaces
{
    public interface INewsReader
    {
        public Task<List<int>> GetNewsIds();
        public Task<HackerNewsArticle> GetNewsArticle(int id);
    }
}
