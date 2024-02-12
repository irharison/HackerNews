namespace HackerNews.NewsReader
{
    public interface INewsRefresher
    {
        void StartTask();
        void StopTask();
        Task<bool> RefreshNewsFeed();
    }
}