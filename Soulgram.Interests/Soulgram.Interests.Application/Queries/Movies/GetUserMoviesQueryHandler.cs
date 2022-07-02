using MediatR;
using Soulgram.Interests.Application.Interfaces.Repositories;
using Soulgram.Interests.Application.Models.Response;

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
        // var moviesIds = await _favoritesRepository.FindOneAsync(
        //     uf => uf.UserId == request.UserId,
        //     favorites => favorites.MoviesIds,
        //     cancellationToken);
        //
        // var getMoviesQuery = new GetMoviesQuery(moviesIds!);
        // return await _mediator.Send(getMoviesQuery, cancellationToken);
        throw new NotImplementedException();
    }
}