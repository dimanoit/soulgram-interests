using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Interfaces;

public interface IUserFavoritesRepository : IRepository<UserFavorites>
{
    Task AddOrCreateFavorites<TCollection>(
        string userId,
        string[] collectionItemsIds,
        CancellationToken cancellationToken);
}
