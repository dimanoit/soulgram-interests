using MediatR;
using Soulgram.Interests.Application.Interfaces.Repositories;
using Soulgram.Interests.Application.Models.Response;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Queries.Movies;

public class GetUserMoviesQueryHandler : IRequestHandler<GetUserMoviesQuery, ICollection<MovieSearchResponse>?>
{
    private readonly IUserFavoritesRepository _favoritesRepository;
    private readonly IMediator _mediator;

    public GetUserMoviesQueryHandler(
        IMediator mediator,
        IUserFavoritesRepository favoritesRepository)
    {
        _favoritesRepository = favoritesRepository;
        _mediator = mediator;
    }

    public async Task<ICollection<MovieSearchResponse>?> Handle(
        GetUserMoviesQuery request,
        CancellationToken cancellationToken)
    {
        var nestedIds = await _favoritesRepository.FindOneAsync(
            uf => uf.UserId == request.UserId,
            favorites => favorites.Interests
                .Where(i => i.Type == InterestGroupType.MovieName)
                .Select(i => i.Ids),
            cancellationToken);

        var ids = nestedIds?.SelectMany(i => i) ?? Array.Empty<string>();
        var getMoviesQuery = new GetMoviesQuery(ids.ToArray());
        return await _mediator.Send(getMoviesQuery, cancellationToken);
    }
}