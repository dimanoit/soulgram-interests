using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Interfaces;

public interface IUserFavoritesService
{
    Task<UserFavorites> GetUserFavorites(
        string userId,
        CancellationToken cancellationToken);

    Task UpsertFavorites(
        UserFavorites favorites,
        CancellationToken cancellationToken);
}