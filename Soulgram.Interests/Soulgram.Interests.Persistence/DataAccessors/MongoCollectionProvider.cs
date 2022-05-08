using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Soulgram.Interests.Persistence;

public class MongoCollectionProvider<TDocument> : IMongoCollectionProvider<TDocument>
{
    public MongoCollectionProvider(
        IMongoClient mongoClient,
        IOptions<InterestsDbSettings> settings)
    {
        var interestsDbSettings = settings ?? throw new ArgumentNullException(nameof(settings));
        var dbClient = mongoClient ?? throw new ArgumentNullException(nameof(mongoClient));

        var database = dbClient.GetDatabase(interestsDbSettings.Value.DatabaseName)
                       ?? throw new ArgumentNullException(nameof(IMongoDatabase));

        var collectionName = typeof(TDocument).Name;
        MongoCollection = database.GetCollection<TDocument>(collectionName);
    }

    public IMongoCollection<TDocument> MongoCollection { get; }
}