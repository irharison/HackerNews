using HackerNews.HackerNewsDto;
using HackerNews.Interfaces;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HackerNews.NewsReader
{
    public class NewsReader : INewsReader
    {
        public IConfiguration Configuration { get; }
        public ILogger<NewsReader> Logger { get; }

        public NewsReader(IConfiguration configuration, ILogger<NewsReader> logger)
        {
            this.Configuration = configuration;
            this.Logger = logger;
        }
     

        public async Task<HackerNewsArticle> GetNewsArticle(int id)
        {
            HackerNewsArticle article = null;
            HttpClient client = new HttpClient();

            try
            {
                string url = Configuration["HackerNewsStoryEndpoint"];
                url = url + id.ToString() + ".json";

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    article = await response.Content.ReadFromJsonAsync<HackerNewsArticle>();
                }
                else
                {
                    return null;
                }

                return article;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error occured while restrieving news article id {id}:{ex.Message}");
                return null;
            }
        }

        public async Task<List<int>> GetNewsIds()
        {
            List<int> idList = new List<int>();
            HttpClient client = new HttpClient();

            try
            {
                string url = Configuration["HackerNewsStoryIdEndpoint"];
                
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    idList = await response.Content.ReadFromJsonAsync<List<int>>();
                }
                else
                {
                    Logger.LogError($"Error occured while retrieving news id list : status code id {response.StatusCode}");
                    return idList;
                }

                return idList;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error occured while retrieving news id list :{ex.Message}");
                throw ex;
            }
        }
    }
}
