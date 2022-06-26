using MediatR;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Application.Interfaces.Repositories;
using Soulgram.Interests.Application.Models.Response;

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

    public async Task<ICollection<GenreResponse>> Handle(
        GetUserGenresQuery request,
        CancellationToken cancellationToken)
    {
        var genres = await _favoritesRepository.Get(
            request.UserId ?? string.Empty,
            projection => projection.GenresIds,
            cancellationToken);

        var genresQuery = new GetGenresQuery(genres);
        
        return await _mediator.Send(genresQuery, cancellationToken);
    }
}
