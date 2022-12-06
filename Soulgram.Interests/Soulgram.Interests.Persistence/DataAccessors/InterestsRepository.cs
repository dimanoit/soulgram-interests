using MongoDB.Driver;
using Soulgram.Interests.Application.Interfaces.Repositories;
using Soulgram.Interests.Domain;
using Soulgram.Mongo.Repository.Interfaces;

namespace Soulgram.Interests.Persistence.DataAccessors;

public class InterestsRepository : GenericRepository<Interest>, IInterestsRepository
{
    public InterestsRepository(IMongoConnection connection)
        : base(connection) { }

    public async Task AddUserToInterests(
        string userId,
        string interestId,
        CancellationToken cancellationToken)
    {
        var updateDefinition = Builders<Interest>.Update.Push(ui => ui.UsersIds, userId);
        await Collection.UpdateOneAsync(
            i => i.Id == interestId,
            updateDefinition,
            cancellationToken: cancellationToken);
    }
}