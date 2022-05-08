using MongoDB.Driver;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Domain;

namespace Soulgram.Interests.Persistence;

public class GenreRepository : MongoRepository<Genre>, IGenreRepository
{
    public GenreRepository(IMongoCollectionProvider<Genre> collectionProvider)
        : base(collectionProvider)
    {
    }

    public async Task AddUserIdToGenre(string genreId, string userId)
    {
        var update = Builders<Genre>.Update.Push(g => g.UsersIds, userId);

        await Collection.FindOneAndUpdateAsync(g => g.Id == genreId, update);
    }
}