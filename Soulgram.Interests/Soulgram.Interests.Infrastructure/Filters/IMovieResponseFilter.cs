using Soulgram.Interests.Application.Models.Response;

namespace Soulgram.Interests.Infrastructure.Filters;

public interface IMovieResponseFilter
{
    public IEnumerable<MovieSearchResponse> Filter(int limit, IEnumerable<MovieSearchResponse>? models);
}