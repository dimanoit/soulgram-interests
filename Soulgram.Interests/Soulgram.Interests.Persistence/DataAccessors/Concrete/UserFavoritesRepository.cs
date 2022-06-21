using System.Linq.Expressions;
using MongoDB.Driver;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Domain;
using Soulgram.Interests.Persistence.Interfaces;

namespace Soulgram.Interests.Persistence.DataAccessors.Concrete;

public class UserFavoritesRepository : MongoRepository<UserFavorites>, IUserFavoritesRepository
{
    public UserFavoritesRepository(IMongoConnection connection)
        : base(connection)
    {
    }

    public async Task<string> GetId(string userId, CancellationToken cancellationToken)
    {
        return await FindOneAsync(
            userFavorites => userFavorites.UserId == userId,
            projection => projection.UserId,
            cancellationToken);
    }

    //TODO refactor this
    public async Task PushAsync(UserFavorites userFavorites, CancellationToken cancellationToken)
    {
        if (userFavorites.GenresIds.Any())
            await PushAsync(userFavorites.UserId, u => u.GenresIds, userFavorites.GenresIds, cancellationToken);

        if (userFavorites.InterestsIds.Any())
            await PushAsync(userFavorites.UserId, u => u.InterestsIds, userFavorites.InterestsIds, cancellationToken);

        if (userFavorites.MoviesIds.Any())
            await PushAsync(userFavorites.UserId, u => u.MoviesIds, userFavorites.MoviesIds, cancellationToken);
    }

    private async Task PushAsync(
        string userId,
        Expression<Func<UserFavorites, IEnumerable<string>>> arrayToUpdate,
        string[] arrayWithUpdate,
        CancellationToken cancellationToken)
    {
        var update = Builders<UserFavorites>.Update.PushEach(arrayToUpdate, arrayWithUpdate);
        await Collection.FindOneAndUpdateAsync(uf => uf.UserId == userId, update, cancellationToken: cancellationToken);
    }
}