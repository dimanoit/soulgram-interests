using MediatR;
using Soulgram.Interests.Application.Interfaces.Repositories;
using Soulgram.Interests.Application.Models.Response;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Queries.Genres;

public record GetUserGenresQuery(string? UserId)
    : IRequest<ICollection<GenreResponse>>;

internal class GetUserGenresQueryHandler
    : IRequestHandler<GetUserGenresQuery, ICollection<GenreResponse>>
{
    private readonly IUserFavoritesRepository _favoritesRepository;
    private readonly IMediator _mediator;

    public GetUserGenresQueryHandler(
        IUserFavoritesRepository favoritesRepository,
        IMediator mediator)
    {
        _favoritesRepository = favoritesRepository;
        _mediator = mediator;
    }
    
    // TODO refactor this
    public async Task<ICollection<GenreResponse>> Handle(
        GetUserGenresQuery request,
        CancellationToken cancellationToken)
    {
        if(request.UserId is null)
        {
            return await _mediator.Send(new GetGenresQuery(null), cancellationToken);
        }
        var genresIds = await _favoritesRepository.Get(
            request.UserId,
            projection => projection
                .Interests
                .Where(i => i.Type == InterestGroupType.MovieGenre)
                .Select(i => i.Ids),
            cancellationToken);

        var genresIdsArray = genresIds?.SelectMany(i => i) ?? Array.Empty<string>();
        var genresQuery = new GetGenresQuery(genresIdsArray.ToArray());
        
        return await _mediator.Send(genresQuery, cancellationToken);
    }
}