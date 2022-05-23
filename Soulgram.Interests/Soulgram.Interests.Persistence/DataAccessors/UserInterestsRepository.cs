using MongoDB.Driver;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Domain;
using Soulgram.Interests.Persistence.Interfaces;

namespace Soulgram.Interests.Persistence.DataAccessors;

public class UserInterestsRepository : MongoRepository<UserInterests>, IUserInterestsRepository
{
    public UserInterestsRepository(IMongoCollectionProvider<UserInterests> collectionProvider)
        : base(collectionProvider)
    {
    }

    public async Task AddInterestToUserInterests(string userId, params InterestType[] interest)
    {
        var update = Builders<UserInterests>.Update.PushEach(userInterests => userInterests.Interests, interest);

        await Collection
            .FindOneAndUpdateAsync(userInterests => userInterests.UserId == userId, update);
    }
}