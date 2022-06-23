using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Services;

public class UserFavoritesService : IUserFavoritesService
{
    private readonly IUserFavoritesRepository _userFavoritesRepository;

    public UserFavoritesService(IUserFavoritesRepository userFavoritesRepository)
    {
        _userFavoritesRepository = userFavoritesRepository;
    }

    public async Task UpsertFavorites(UserFavorites favorites, CancellationToken cancellationToken)
    {
        var favoriteId = await _userFavoritesRepository.GetId(favorites.UserId, cancellationToken);

        if (string.IsNullOrEmpty(favoriteId))
        {
            await _userFavoritesRepository.InsertOneAsync(favorites, cancellationToken);
            return;
        }

        await _userFavoritesRepository.PushAsync(favorites, cancellationToken);
    }
}