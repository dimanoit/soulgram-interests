using MediatR;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Commands.Movies.Handlers;

public class AddMovieCommandHandler : IRequestHandler<AddMovieCommand>
{
    private readonly IMediator _mediator;
    private readonly IRepository<Movie> _movieRepository;
    private readonly IUserFavoritesRepository _userFavoritesRepository;


    public AddMovieCommandHandler(IRepository<Movie> movieRepository, IMediator mediator, IUserFavoritesRepository userFavoritesRepository)
    {
        _movieRepository = movieRepository;
        _mediator = mediator;
        _userFavoritesRepository = userFavoritesRepository;
    }

    public async Task<Unit> Handle(AddMovieCommand command, CancellationToken cancellationToken)
    {
        var movieId = await GetMovieId(command.Request.ImdbId, cancellationToken);
        if (string.IsNullOrEmpty(movieId))
        {
            var createMovieCommand = new CreateMovieCommand(command.Request);
            if (!string.IsNullOrEmpty(command.UserId))
            {
                createMovieCommand.UserId = command.UserId;
            }
            await _mediator.Send(createMovieCommand, cancellationToken);

            movieId = await GetMovieId(command.Request.ImdbId, cancellationToken);
        }
        
        // TODO make separate service due to code duplication
        var userFavorites = new UserFavorites
        {
            UserId = command.UserId,
            MoviesIds = new []{ movieId! }
        };

        var favoriteId = await _userFavoritesRepository.GetId(command.UserId, cancellationToken);
        if (string.IsNullOrEmpty(favoriteId))  await _userFavoritesRepository.InsertOneAsync(userFavorites, cancellationToken);
        else await _userFavoritesRepository.PushAsync(userFavorites, cancellationToken);
        
        
        return Unit.Value;
    }

    private async Task<string?> GetMovieId(string imdbId, CancellationToken cancellationToken)
    {
        var movieId = await _movieRepository.FindOneAsync(
            movie => movie.ImdbId == imdbId,
            movie => movie.Id,
            cancellationToken);

        return movieId;
    }
}