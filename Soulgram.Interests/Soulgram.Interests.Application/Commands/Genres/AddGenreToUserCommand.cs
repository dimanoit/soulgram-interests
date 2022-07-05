using MediatR;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Application.Interfaces.Repositories;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Commands.Genres;

public record AddGenreToUserCommand(string GenreId, string UserId) : IRequest;

internal class AddUserToGenreCommandHandler
    : IRequestHandler<AddGenreToUserCommand>
{
    private readonly IUserFavoritesService _favoritesService;
    private readonly IGenreRepository _genreRepository;

    public AddUserToGenreCommandHandler(
        IGenreRepository genreRepository,
        IUserFavoritesService favoritesService)
    {
        _genreRepository = genreRepository;
        _favoritesService = favoritesService;
    }

    public async Task<Unit> Handle(AddGenreToUserCommand command, CancellationToken cancellationToken)
    {
        var userFavorites = new UserFavorites
        {
            UserId = command.UserId,
            Interests = new[]
            {
                new InterestsIds()
                {
                    Type = InterestGroupType.MovieGenre,
                    Ids = new[] { command.GenreId } 
                }
            }
        };

        await _favoritesService.UpsertFavorites(userFavorites, cancellationToken);
        
        await _genreRepository.AddUserIdToGenre(
            command.GenreId,
            command.UserId);
        
        return Unit.Value;
    }
}