using MongoDB.Driver;
using Soulgram.Interests.Application.Interfaces.Repositories;
using Soulgram.Interests.Domain;
using Soulgram.Mongo.Repository.Interfaces;
using System.Linq.Expressions;

namespace Soulgram.Interests.Persistence.DataAccessors;

public class UserFavoritesRepository : GenericRepository<UserFavorites>, IUserFavoritesRepository
{
    public UserFavoritesRepository(IMongoConnection connection)
        : base(connection)
    {
    }

    public async Task<T?> Get<T>(
        string userId,
        Expression<Func<UserFavorites, T?>> projection,
        CancellationToken cancellationToken)
    {
        return await FindOneAsync(
            userFavorites => userFavorites.UserId == userId,
            projection,
            cancellationToken);
    }

    public async Task AddToInterestsIds(
        string favoriteId,
        InterestsIds interestsIds,
        CancellationToken cancellationToken)
    {
        var interest = await FindOneAsync(
            uf => uf.Id == favoriteId && uf.Interests.Any(i => i.Type == interestsIds.Type),
            uf => uf.Interests.Select(i => i.Type),
            cancellationToken) ?? Enumerable.Empty<InterestGroupType>();

        if (!interest.Any())
        {
            var update = Builders<UserFavorites>.Update.Push(e => e.Interests, interestsIds);

            await Collection.FindOneAndUpdateAsync(
                uf => uf.Id == favoriteId,
                update,
                cancellationToken: cancellationToken);

            return;
        }

        await PushToSpecificInterest(favoriteId, interestsIds, cancellationToken);
    }

    private async Task PushToSpecificInterest(
        string favoritesIds,
        InterestsIds interestsIds,
        CancellationToken cancellationToken)
    {
        var filter =
            Builders<UserFavorites>.Filter.Eq(uf => uf.Id, favoritesIds) &
            Builders<UserFavorites>.Filter.ElemMatch(e => e.Interests,
                Builders<InterestsIds>.Filter.Eq(i => i.Type, interestsIds.Type));

        var update = Builders<UserFavorites>.Update.PushEach(e => e.Interests[-1].Ids, interestsIds.Ids);
        await Collection.FindOneAndUpdateAsync(filter, update, cancellationToken: cancellationToken);
    }
}