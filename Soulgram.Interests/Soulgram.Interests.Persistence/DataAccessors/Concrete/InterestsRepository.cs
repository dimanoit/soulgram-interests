using MongoDB.Driver;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Domain;
using Soulgram.Interests.Persistence.Interfaces;

namespace Soulgram.Interests.Persistence.DataAccessors.Concrete;

public class InterestsRepository : MongoRepository<Interest>, IInterestsRepository
{
    public InterestsRepository(IMongoConnection connection)
        : base(connection) { }

    public async Task AddUserToInterests(
        string userId,
        string[] interestsIds,
        CancellationToken cancellationToken)
    {
        var updateDefinition = Builders<Interest>.Update.Push(ui => ui.UsersIds, userId);
        await Collection.UpdateManyAsync(
            i => interestsIds.Contains(i.Id),
            updateDefinition,
            cancellationToken: cancellationToken);
    }
}