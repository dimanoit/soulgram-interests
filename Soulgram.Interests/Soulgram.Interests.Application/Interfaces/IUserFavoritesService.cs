using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Interfaces;

public interface IUserFavoritesService
{
    Task UpsertFavorites(UserFavorites favorites, CancellationToken cancellationToken);
}