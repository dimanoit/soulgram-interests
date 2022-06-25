using MediatR;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Application.Models.Request;
using Soulgram.Interests.Application.Models.Response;

namespace Soulgram.Interests.Application.Queries.Movies;

public class SearchMoviesByNameQuery : IRequest<IEnumerable<MovieSearchResponse>>
{
    public SearchMoviesByNameQuery(SearchMovieRequest request)
    {
        Request = request;
    }

    public SearchMovieRequest Request { get; }
}

internal class SearchMoviesByNameQueryHandler
    : IRequestHandler<SearchMoviesByNameQuery, IEnumerable<MovieSearchResponse>>
{
    private readonly IMovieService _movieService;

    public SearchMoviesByNameQueryHandler(IMovieService movieService)
    {
        _movieService = movieService;
    }

    public async Task<IEnumerable<MovieSearchResponse>> Handle(
        SearchMoviesByNameQuery query,
        CancellationToken cancellationToken)
    {
        return await _movieService.GetMoviesByName(query.Request, cancellationToken);
    }
}