using HackerNews.ApiDto;
using HackerNews.Interfaces;

namespace HackerNews.NewsReader
{
    public class NewsStoreLock :INewsStoreLock
    {

        INewsStore _newsStore;
        private ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        public NewsStoreLock(INewsStore newsStore)
        {
            _newsStore = newsStore;
        }

        public List<NewsArticle> GetArticles(int articleCount)
        {
            try
            {
                _lock.EnterReadLock();
                return _newsStore.GetArticles(articleCount);
            }
            finally
            {
                _lock.ExitReadLock();            
            }
        }
        public List<int> GetMissingArticles(List<int> newArticleList)
        {
            try
            {
                _lock.EnterReadLock();
                return _newsStore.GetMissingArticles(newArticleList);
            }
            finally 
            { 
                _lock.ExitReadLock(); 
            }
        }


        public void UpdateArticleList(Dictionary<int, NewsArticle> newArticles)
        {
            try
            {
                _lock.EnterWriteLock();
                foreach (var article in newArticles)
                {
                    _newsStore.AddArticle(article.Key, article.Value);
                }
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }
    }
}
