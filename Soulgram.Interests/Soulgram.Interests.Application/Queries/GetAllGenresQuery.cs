using MediatR;

namespace Soulgram.Interests.Application;

public class GetAllGenresQuery : IRequest<IEnumerable<string>>
{
}

internal class GetGenresForSearchQueryHandler : IRequestHandler<GetAllGenresQuery, IEnumerable<string>>
{
    private readonly IMovieService _movieService;

    public GetGenresForSearchQueryHandler(IMovieService movieService)
    {
        _movieService = movieService;
    }

    public async Task<IEnumerable<string>> Handle(GetAllGenresQuery request, CancellationToken cancellationToken)
    {
        var genres = await _movieService.GetGenresAsync(cancellationToken);

        return genres;
    }
}