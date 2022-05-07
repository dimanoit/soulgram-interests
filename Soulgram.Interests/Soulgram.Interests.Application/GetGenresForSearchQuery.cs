using MediatR;

namespace Soulgram.Interests.Application;

public class GetGenresForSearchQuery : IRequest<IEnumerable<string>>
{
}

internal class GetGenresForSearchQueryHandler : IRequestHandler<GetGenresForSearchQuery, IEnumerable<string>>
{
    private readonly IMovieService _movieService;

    public GetGenresForSearchQueryHandler(IMovieService movieService)
    {
        _movieService = movieService;
    }

    public async Task<IEnumerable<string>> Handle(GetGenresForSearchQuery request, CancellationToken cancellationToken)
    {
        var genres = await _movieService.GetGenresAsync(cancellationToken);

        return genres;
    }
}