using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Application.Interfaces;

public interface IUserFavoritesRepository : IRepository<UserFavorites>
{
    Task<string> GetId(string userId, CancellationToken cancellationToken);

    Task PushAsync(UserFavorites userFavorites, CancellationToken cancellationToken);
}