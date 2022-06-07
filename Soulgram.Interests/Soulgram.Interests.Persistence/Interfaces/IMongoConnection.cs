using MongoDB.Driver;

namespace Soulgram.Interests.Persistence.Interfaces;

public interface IMongoConnection
{
    Task<IClientSessionHandle> Session { get; }
    IMongoCollection<TDocument> GetMongoCollection<TDocument>();
}