using Microsoft.AspNetCore.Mvc;
using HackerNews.ApiDto;
using HackerNews.Interfaces;

namespace HackerNews.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HackerNewsController : ControllerBase
    {
        private readonly ILogger<HackerNewsController> _logger;
        private readonly INewsStoreLock _newsStoreLock;

        public HackerNewsController(ILogger<HackerNewsController> logger, INewsStoreLock newsStoreLock)
        {
            _logger = logger;
            this._newsStoreLock = newsStoreLock;
        }

        [HttpGet(Name = "GetNews")]
        public IEnumerable<NewsArticle> Get(int count)
        {
            return _newsStoreLock.GetArticles(count);
        }
    }
}