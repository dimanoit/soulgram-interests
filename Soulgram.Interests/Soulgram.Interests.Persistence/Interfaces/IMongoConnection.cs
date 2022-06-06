using MongoDB.Driver;

namespace Soulgram.Interests.Persistence.Interfaces;

public interface IMongoConnection<TDocument>
{
    Task<IClientSessionHandle> Session { get; }
    IMongoCollection<TDocument> MongoCollection { get; }
}