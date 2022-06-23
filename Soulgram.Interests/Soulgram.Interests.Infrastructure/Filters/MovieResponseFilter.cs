using Soulgram.Interests.Application.Models.Response;

namespace Soulgram.Interests.Infrastructure.Filters;

public class MovieResponseFilter : IMovieResponseFilter
{
    public IEnumerable<MovieSearchResponse> Filter(int limit, IEnumerable<MovieSearchResponse>? models)
    {
        if (models == null)
        {
            return Enumerable.Empty<MovieSearchResponse>();
        }
        
        return models
            .Where(r => !string.IsNullOrEmpty(r.BriefDescription))
            .Where(r => r.ImgUrls != null && r.ImgUrls.Any(img => !string.IsNullOrEmpty(img)))
            .Take(limit);
    }
}