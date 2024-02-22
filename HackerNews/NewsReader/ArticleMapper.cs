using HackerNews.ApiDto;
using HackerNews.HackerNewsDto;
using HackerNews.Utilities;

namespace HackerNews.NewsReader
{
    public class ArticleMapper
    {
        public NewsArticle MapArticle(HackerNewsArticle articleToMap)
        {
            return new NewsArticle
            {
                Title = articleToMap.Title,
                Url = articleToMap.Url,
                PostedBy = articleToMap.By,
                Time = UnixTimeStampConvertor.UnixTimeStampToDateTime(articleToMap.Time),
                Score = articleToMap.Score,
                CommentCount = articleToMap.Kids.Length
            };

        }
    }
}
