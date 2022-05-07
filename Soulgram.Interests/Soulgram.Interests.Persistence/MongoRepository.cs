using System.Linq.Expressions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Soulgram.Interests.Application;

namespace Soulgram.Interests.Persistence;

public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : class
{
    private readonly IMongoCollection<TDocument> _collection;

    public MongoRepository(
        IMongoClient mongoClient,
        IOptions<InterestsDbSettings> settings)
    {
        var interestsDbSettings = settings ?? throw new ArgumentNullException(nameof(settings));
        var dbClient = mongoClient ?? throw new ArgumentNullException(nameof(mongoClient));

        var database = dbClient.GetDatabase(interestsDbSettings.Value.DatabaseName);
        _collection = database.GetCollection<TDocument>(nameof(TDocument));
    }

    public async Task<ICollection<TDocument>> FilterByAsync(
        Expression<Func<TDocument, bool>> filterExpression)
    {
        return await _collection.Find(filterExpression).ToListAsync();
    }

    public async Task<ICollection<TProjected>> FilterByAsync<TProjected>(
        Expression<Func<TDocument, bool>> filterExpression,
        Expression<Func<TDocument, TProjected>> projectionExpression)
    {
        return await _collection.Find(filterExpression).Project(projectionExpression).ToListAsync();
    }

    public async Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression)
    {
        return await _collection.Find(filterExpression).FirstOrDefaultAsync();
    }

    public async Task InsertOneAsync(TDocument document)
    {
        await _collection.InsertOneAsync(document);
    }

    public async Task InsertManyAsync(ICollection<TDocument> documents)
    {
        await _collection.InsertManyAsync(documents);
    }

    public async Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression)
    {
        await _collection.FindOneAndDeleteAsync(filterExpression);
    }

    public async Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression)
    {
        await _collection.DeleteManyAsync(filterExpression);
    }
}