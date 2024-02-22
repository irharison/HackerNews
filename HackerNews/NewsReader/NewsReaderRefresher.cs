using HackerNews.ApiDto;
using HackerNews.HackerNewsDto;
using HackerNews.Interfaces;
using System.Threading;

namespace HackerNews.NewsReader
{
    public class NewsReaderRefresher : INewsRefresher
    {
        private readonly IConfiguration _configuration;
        private readonly INewsReader _newsReader;
        private readonly INewsStoreLock _newsStoreLock;
        private Task _pollingTask;
        private readonly int _pollingInterval;
        private readonly int _refreshCountLimit;
        private int _currentPollcount;
        private CancellationToken _cancellationToken;
        private CancellationTokenSource _cancellationTokenSource;
        private readonly ILogger<NewsReaderRefresher> _logger;

        public NewsReaderRefresher(IConfiguration configuration, INewsReader newsReader, INewsStoreLock newsStoreLock, ILogger<NewsReaderRefresher> logger)
        {
            this._configuration = configuration;
            this._newsReader = newsReader;
            this._newsStoreLock = newsStoreLock;
            _pollingInterval = int.Parse(configuration["PollingInterval"]);
            _refreshCountLimit = int.Parse(configuration["RefreshCountLimit"]);
            _currentPollcount = 0; 
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            _logger = logger;
        }

        public void StartTask()
        {
            //Make sure we get an initial list before we start polling, so we block when starting up
            var refreshResult = RefreshNewsFeed().Result;
            _pollingTask = new Task(() => PollForRefresh());
            _pollingTask.Start();
        }

        public void StopTask()
        {
            _cancellationTokenSource.Cancel();
        }

        public async Task PollForRefresh()
        {
            while (true)
            {
                if (_currentPollcount >= _refreshCountLimit)
                {
                    _currentPollcount = 0;
                    var result = await RefreshNewsFeed();  
                }
                else
                {
                    _currentPollcount++;
                }

                _cancellationToken.ThrowIfCancellationRequested();
                await Task.Delay(_pollingInterval);
            }
        }
        
        public async Task<bool> RefreshNewsFeed()
        {
            try
            {
                //Get List of news stories
                var list = await _newsReader.GetNewsIds();
                ArticleMapper mapper = new ArticleMapper();

                //Get list of missing articles
                var missingArticles = _newsStoreLock.GetMissingArticles(list);

                if (missingArticles.Count > 0)
                {
                    Dictionary<int, NewsArticle> articlesToAdd = new Dictionary<int, NewsArticle>();

                    foreach (var articleId in missingArticles)
                    {
                        HackerNewsArticle article = await _newsReader.GetNewsArticle(articleId);
                        articlesToAdd.Add(articleId, mapper.MapArticle(article));
                    }

                    //We update all the articles at once, so that writelock is only 
                    //established for the shortest amount of time.
                    _newsStoreLock.UpdateArticleList(articlesToAdd);
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured when trying to retrieve articles from HackerNews : {ex.Message}");
            }

            return false;
        }

    }
}
