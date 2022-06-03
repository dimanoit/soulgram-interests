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
        var updateDefinition = Builders<UserInterests>.Update.Push(ui => ui.UsersIds, userId);
        await UpdateDocument(interestId, updateDefinition);
    }

    public async Task AddUserToInterestBulk(string[] userId, string interestId)
    {
        var updateDefinition = Builders<UserInterests>.Update.PushEach(userInterests => userInterests.UsersIds, userId);
        await UpdateDocument(interestId, updateDefinition);
    }

    public async Task AddInterestsToOneUser(string userId, string[] interestsIds)
    {
        var tasks = interestsIds.Select(id => AddUserToInterest(userId, id));
        await Task.WhenAll(tasks);
    }

    private async Task UpdateDocument(string interestId, UpdateDefinition<UserInterests> update)
    {
        await Collection
            .FindOneAndUpdateAsync(userInterests => userInterests.Id == interestId,
                update);
    }
}