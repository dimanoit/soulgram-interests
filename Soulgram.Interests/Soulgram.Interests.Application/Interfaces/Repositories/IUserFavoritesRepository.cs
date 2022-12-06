using Soulgram.Interests.Domain;
using System.Linq.Expressions;

namespace Soulgram.Interests.Application.Interfaces.Repositories;

public interface IUserFavoritesRepository : IRepository<UserFavorites>
{
    public Task<T?> Get<T>(
        string userId,
        Expression<Func<UserFavorites, T?>> projection,
        CancellationToken cancellationToken);

    public Task AddToInterestsIds(
        string favoriteId,
        InterestsIds interestsIds,
        CancellationToken cancellationToken);
}