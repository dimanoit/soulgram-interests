using MongoDB.Driver;

namespace Soulgram.Interests.Persistence.Interfaces;

public interface IMongoCollectionProvider<TDocument>
{
    IMongoCollection<TDocument> MongoCollection { get; }
}