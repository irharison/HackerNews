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
                title = articleToMap.title,
                uri = articleToMap.url,
                postedBy = articleToMap.by,
                time = UnixTimeStampConvertor.UnixTimeStampToDateTime(articleToMap.time),
                score = articleToMap.score,
                commentCount = articleToMap.kids.Length
            };

        }
    }
}
