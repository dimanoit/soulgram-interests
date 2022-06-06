using MongoDB.Driver;

namespace Soulgram.Interests.Persistence.Interfaces;

public interface IMongoConnection<TDocument>
{
    IMongoCollection<TDocument> MongoCollection { get; }
}