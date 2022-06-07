using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Soulgram.Interests.Persistence.Interfaces;
using Soulgram.Interests.Persistence.Models;

namespace Soulgram.Interests.Persistence.DataAccessors;

public class MongoConnection : IMongoConnection
{
    private readonly IMongoClient _mongoClient;

    public MongoConnection(
        IMongoClient mongoClient,
        IOptions<InterestsDbSettings> settings)
    {
        var interestsDbSettings = settings ?? throw new ArgumentNullException(nameof(settings));
        _mongoClient = mongoClient ?? throw new ArgumentNullException(nameof(mongoClient));

        Database = _mongoClient.GetDatabase(interestsDbSettings.Value.DatabaseName)
                   ?? throw new ArgumentNullException(nameof(IMongoDatabase));
    }

    private IMongoDatabase Database { get; }

    public Task<IClientSessionHandle> Session => _mongoClient.StartSessionAsync();

    public IMongoCollection<TDocument> GetMongoCollection<TDocument>()
    {
        var collectionName = typeof(TDocument).Name;
        return Database.GetCollection<TDocument>(collectionName);
    }
}