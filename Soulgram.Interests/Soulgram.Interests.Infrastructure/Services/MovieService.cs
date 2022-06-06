using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Application.Models.Request;
using Soulgram.Interests.Application.Models.Response;
using Soulgram.Interests.Infrastructure.Facades;
using Soulgram.Interests.Infrastructure.Filters;

namespace Soulgram.Interests.Infrastructure.Services;

public class MovieService : IMovieService
{
    private readonly IMovieFacade _facade;
    private readonly IMovieResponseFilter _filter;
    public MovieService(IMovieFacade facade, IMovieResponseFilter filter)
    {
        _facade = facade;
        _filter = filter;
    }

    public async Task<ICollection<string>> GetGenresAsync(CancellationToken cancellationToken)
    {
        var result = await _facade.GetGenresAsync(cancellationToken);

        if (result == null || result.Count == 0) return Array.Empty<string>();

        return result.Where(n => !string.IsNullOrEmpty(n)).ToArray();
    }

    public async Task<IEnumerable<MovieSearchResponse>> GetMoviesByName(
        SearchMovieRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _facade.GetMoviesByName(request, cancellationToken);
        return _filter.Filter(request.Limit, result);
    }
}