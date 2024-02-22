using HackerNews.ApiDto;
using HackerNews.HackerNewsDto;
using HackerNews.Interfaces;
using HackerNews.NewsReader;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace HackerNewsTest
{
    public class NewsReaderRefresherTest
    {
        IConfiguration _config;

        public NewsReaderRefresherTest()
        {
            var configSettings = new Dictionary<string, string>
            {
                { "PollingInterval", "10" },
                { "RefreshCountLimit", "20" }
            };

            _config = new ConfigurationBuilder().AddInMemoryCollection(configSettings).Build();
        }
        [Fact]
        public void RefreshNewsFeedTestAddsNewListOfArticles()
        {
            var expectedList = new List<int> { 1, 2 };
            
            Mock<ILogger<NewsReaderRefresher>> mockLogger = new Mock<ILogger<NewsReaderRefresher>>();
            TestNewsStore testNewsStore = new TestNewsStore();

            var refresher = new NewsReaderRefresher(_config, new TestNewsReader(), testNewsStore, mockLogger.Object);

            refresher.RefreshNewsFeed();

            Assert.Equal(2, testNewsStore.ArticlesAdded.Count);
            Assert.True(testNewsStore.ArticlesAdded.Contains(1));
            Assert.True(testNewsStore.ArticlesAdded.Contains(2));
        }
    }

    public class TestNewsStore : INewsStoreLock
    {
        public List<int> ArticlesAdded = new List<int>();

        public List<NewsArticle> GetArticles(int articleCount)
        {
            throw new System.NotImplementedException();
        }

        public List<int> GetMissingArticles(List<int> newArticleList)
        {
            return new List<int> { 1, 2, };
        }

        public void UpdateArticleList(Dictionary<int, NewsArticle> newArticles)
        {
            foreach (var item in newArticles)
            {
                ArticlesAdded.Add(item.Key);
            }
        }

    }

    public class TestNewsReader : INewsReader
    {
        public Task<HackerNewsArticle> GetNewsArticle(int identifier)
        {
            return Task.FromResult(new HackerNewsArticle
            {
                Id = identifier,
                By = string.Empty,
                Descendents = 0,
                Kids = new int[0],
                Score = 0,
                Story = string.Empty,
                Time = 0,
                Title = string.Empty,
                Url = string.Empty
            }); ;
        }

        public Task<List<int>> GetNewsIds()
        {
            return Task.FromResult(new List<int>() { 1,2 });
        }
    }
}