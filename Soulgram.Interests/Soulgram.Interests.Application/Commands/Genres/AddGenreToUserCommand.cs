using System.ComponentModel.DataAnnotations;
using MediatR;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Application.Interfaces.Repositories;
using Soulgram.Interests.Application.Models.Request;
using Soulgram.Interests.Application.Models.Request.Genres;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Commands.Genres;

public record AddGenreToUserCommand(string GenreId, string UserId) : IRequest;

internal class AddUserToGenreCommandHandler 
    : IRequestHandler<AddGenreToUserCommand>
{
    private readonly IGenreRepository _genreRepository;
    private readonly IUserFavoritesService _favoritesService;

    public AddUserToGenreCommandHandler(IGenreRepository genreRepository, IUserFavoritesService favoritesService)
    {
        _genreRepository = genreRepository;
        _favoritesService = favoritesService;
    }

    public async Task<Unit> Handle(AddGenreToUserCommand command, CancellationToken cancellationToken)
    {
        await _genreRepository.AddUserIdToGenre(
            command.GenreId,
            command.UserId);

        var userFavorites = new UserFavorites()
        {
            UserId = command.UserId,
            GenresIds = new[] {command.GenreId}
        };
        
        await _favoritesService.UpsertFavorites(userFavorites, cancellationToken);
        
        return Unit.Value;
    }
}