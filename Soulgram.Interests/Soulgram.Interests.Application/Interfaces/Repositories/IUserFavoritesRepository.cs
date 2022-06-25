using System.Linq.Expressions;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Interfaces.Repositories;

public interface IUserFavoritesRepository : IRepository<UserFavorites>
{
    public Task<T> Get<T>(
        string userId,
        Expression<Func<UserFavorites, T>> projection,
        CancellationToken cancellationToken);
    
    Task PushAsync(UserFavorites userFavorites, CancellationToken cancellationToken);
}