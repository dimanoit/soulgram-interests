using System.Collections.Immutable;
using System.Linq.Expressions;
using MongoDB.Driver;
using Soulgram.Interests.Application.Interfaces.Repositories;
using Soulgram.Interests.Domain;
using Soulgram.Interests.Persistence.Interfaces;

namespace Soulgram.Interests.Persistence.DataAccessors.Concrete;

public class UserFavoritesRepository : MongoRepository<UserFavorites>, IUserFavoritesRepository
{
    public UserFavoritesRepository(IMongoConnection connection)
        : base(connection) { }

    public async Task<T> Get<T>(
        string userId,
        Expression<Func<UserFavorites, T>> projection,
        CancellationToken cancellationToken)
    {
        return await FindOneAsync(
            userFavorites => userFavorites.UserId == userId,
            projection,
            cancellationToken);
    }

    public async Task AddToInterestsIds(
        string favoriteId,
        InterestGroupType interestType,
        string interestId,
        CancellationToken cancellationToken)
    {
        var interest = await FindOneAsync(
            uf => uf.Id == favoriteId && uf.Interests.Any(i => i.Type == interestType),
            uf => uf.Interests.Select(i => i.Type),
            cancellationToken) ?? Enumerable.Empty<InterestGroupType>();
        
        if (!interest.Any())
        {
            var update = Builders<UserFavorites>.Update.Push(e => e.Interests, 
                new InterestsIds
            {
                Type = interestType,
                Ids = new []{interestId}
            });
            
            await Collection.FindOneAndUpdateAsync(
                uf => uf.Id == favoriteId,
                update,
                cancellationToken: cancellationToken);
            
            return;
        }
        
        await PushToSpecificInterest(favoriteId, interestType, interestId, cancellationToken);
    }

    private async Task PushToSpecificInterest(
        string favoritesIds,
        InterestGroupType interestType,
        string interestId,
        CancellationToken cancellationToken)
    {
        var filter =
            Builders<UserFavorites>.Filter.Eq(uf => uf.Id, favoritesIds) &
            Builders<UserFavorites>.Filter.ElemMatch(e => e.Interests,
                Builders<InterestsIds>.Filter.Eq(i => i.Type, interestType));

        var update = Builders<UserFavorites>.Update.Push(e => e.Interests[-1].Ids, interestId);
        await Collection.FindOneAndUpdateAsync(filter, update, cancellationToken: cancellationToken);
    }
}