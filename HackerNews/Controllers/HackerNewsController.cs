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
        public ActionResult<IEnumerable<NewsArticle>> Get(int count)
        {
            try
            {
                var returnValue = _newsStoreLock.GetArticles(count);
                return Ok(returnValue);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}