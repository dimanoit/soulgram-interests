using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Application.Interfaces.Repositories;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Services;

public class UserFavoritesService : IUserFavoritesService
{
    private readonly IUserFavoritesRepository _userFavoritesRepository;

    public UserFavoritesService(IUserFavoritesRepository userFavoritesRepository)
    {
        _userFavoritesRepository = userFavoritesRepository;
    }

    public async Task<UserFavorites> GetUserFavorites(
        string userId,
        CancellationToken cancellationToken)
    {
        return await _userFavoritesRepository
            .Get(userId,
                userFavorites => userFavorites,
                cancellationToken
            );
    }

    public async Task UpsertFavorites(UserFavorites favorites, CancellationToken cancellationToken)
    {
        var favoriteId = await _userFavoritesRepository.Get(
            favorites.UserId,
            projection => projection.Id,
            cancellationToken);

        if (string.IsNullOrEmpty(favoriteId))
        {
            await _userFavoritesRepository.InsertOneAsync(favorites, cancellationToken);
            return;
        }

        foreach (var interest in favorites.Interests)
        {
            foreach (var id in interest.Ids)
            {
                await _userFavoritesRepository.AddToInterestsIds(
                    favoriteId,
                    interest.Type,
                    id,
                    cancellationToken);
            }
        }
    }
}