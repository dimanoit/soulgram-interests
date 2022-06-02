using Soulgram.Interests.Infrastructure.Models;

namespace Soulgram.Interests.Infrastructure.Filters;

public class MovieResponseFilter : IMovieResponseFilter
{
    private readonly string[] _movieTypes = { "movie", "tvMovie" };
    private readonly int _paginator = 5;

    public IEnumerable<MovieResponseModel> Filter(IEnumerable<MovieResponseModel>? models)
    {
        if (models == null) return Enumerable.Empty<MovieResponseModel>();

        return models
            .Where(mrm => !string.IsNullOrEmpty(mrm.BriefDescription))
            .Where(mrm => _movieTypes.Contains(mrm.Type))
            .Take(_paginator);
    }
}