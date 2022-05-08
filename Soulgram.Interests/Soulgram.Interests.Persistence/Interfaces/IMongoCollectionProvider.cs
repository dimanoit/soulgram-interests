using MongoDB.Driver;

namespace Soulgram.Interests.Persistence;

public interface IMongoCollectionProvider<TDocument>
{
    IMongoCollection<TDocument> MongoCollection { get; }
}