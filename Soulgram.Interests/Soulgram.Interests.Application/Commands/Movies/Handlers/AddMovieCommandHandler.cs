using MediatR;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Commands.Movies.Handlers;

public class AddMovieCommandHandler : IRequestHandler<AddMovieCommand>
{
    private readonly IMediator _mediator;
    private readonly IRepository<Movie> _movieRepository;
    private readonly IUserFavoritesService _userFavoritesService;


    public AddMovieCommandHandler(
        IRepository<Movie> movieRepository,
        IMediator mediator,
        IUserFavoritesService userFavoritesService)
    {
        _movieRepository = movieRepository;
        _mediator = mediator;
        _userFavoritesService = userFavoritesService;
    }

    public async Task<Unit> Handle(AddMovieCommand command, CancellationToken cancellationToken)
    {
        var movieId = await GetMovieId(command.Request.ImdbId, cancellationToken);

        if (string.IsNullOrEmpty(movieId))
        {
            var createMovieCommand = new CreateMovieCommand(command.Request);
            await _mediator.Send(createMovieCommand, cancellationToken);

            movieId = await GetMovieId(command.Request.ImdbId, cancellationToken);
        }

        await UpsertUserFavorites(command, movieId, cancellationToken);

        return Unit.Value;
    }

    private async Task UpsertUserFavorites(
        AddMovieCommand command,
        string? movieId,
        CancellationToken cancellationToken)
    {
        var userFavorites = new UserFavorites
        {
            UserId = command.Request.UserId!,
            Interests = new[]
            {
                new InterestsIds
                {
                    Type = InterestGroupType.MovieName,
                    Ids = new[] {movieId!}
                }
            }
        };

        await _userFavoritesService.UpsertFavorites(userFavorites, cancellationToken);
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