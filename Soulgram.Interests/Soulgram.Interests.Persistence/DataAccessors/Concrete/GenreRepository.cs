using MongoDB.Driver;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Application.Interfaces.Repositories;
using Soulgram.Interests.Domain;
using Soulgram.Interests.Persistence.Interfaces;

namespace Soulgram.Interests.Persistence.DataAccessors.Concrete;

public class GenreRepository : MongoRepository<Genre>, IGenreRepository
{
    public GenreRepository(IMongoConnection connection)
        : base(connection) { }

    public async Task AddUserIdToGenre(string genreId, string userId)
    {
        var update = Builders<Genre>.Update.Push(g => g.UsersIds, userId);
        await Collection.FindOneAndUpdateAsync(g => g.Id == genreId, update);
    }
}