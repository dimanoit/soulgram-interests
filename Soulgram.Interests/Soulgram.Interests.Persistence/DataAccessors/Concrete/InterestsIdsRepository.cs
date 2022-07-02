using MongoDB.Driver;
using Soulgram.Interests.Application.Interfaces.Repositories;
using Soulgram.Interests.Domain;
using Soulgram.Interests.Persistence.Interfaces;

namespace Soulgram.Interests.Persistence.DataAccessors.Concrete;

public class InterestsIdsRepository: MongoRepository<InterestsIds>, IInterestsIdsRepository
{
    public InterestsIdsRepository(IMongoConnection connection)
        : base(connection) { }

    public async Task AddUserIdToGenre(InterestGroupType type, string id)
    {
        var update = Builders<InterestsIds>.Update.Push(i => i.Ids, id);
        await Collection.FindOneAndUpdateAsync(i => i.Type == type, update);
    }
}