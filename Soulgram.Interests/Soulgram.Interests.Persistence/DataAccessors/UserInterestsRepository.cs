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

    public async Task AddUserToInterest(
        string userId,
        string interestId)
    {
        var update = Builders<UserInterests>.Update.Push(userInterests => userInterests.UsersIds, userId);

        await Collection
            .FindOneAndUpdateAsync(userInterests => userInterests.Id == interestId,
                update);
    }
}