using MediatR;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Application.Models.Response;

namespace Soulgram.Interests.Application.Queries;

public class SearchMoviesByNameQuery : IRequest<IEnumerable<MovieSearchResponse>>
{
    public SearchMoviesByNameQuery(string name)
    {
        Name = name;
    }

    public string Name { get; }
}

internal class
    SearchMoviesByNameQueryHandler : IRequestHandler<SearchMoviesByNameQuery, IEnumerable<MovieSearchResponse>>
{
    private readonly IMovieService _movieService;

    public SearchMoviesByNameQueryHandler(IMovieService movieService)
    {
        _movieService = movieService;
    }

    public async Task<IEnumerable<MovieSearchResponse>> Handle(SearchMoviesByNameQuery request,
        CancellationToken cancellationToken)
    {
        return await _movieService.GetMoviesByName(request.Name, cancellationToken);
    }
}