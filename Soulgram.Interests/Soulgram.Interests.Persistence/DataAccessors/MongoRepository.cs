using System.Linq.Expressions;
using MongoDB.Driver;
using Soulgram.Interests.Application.Interfaces;
using Soulgram.Interests.Persistence.Interfaces;

namespace Soulgram.Interests.Persistence.DataAccessors;

public class MongoRepository<TDocument> : IRepository<TDocument> where TDocument : class
{
    public MongoRepository(IMongoCollectionProvider<TDocument> collectionProvider)
    {
        if (collectionProvider == null)
        {
            throw new ArgumentNullException(nameof(collectionProvider));
        }

        Collection = collectionProvider.MongoCollection;
    }

    protected IMongoCollection<TDocument> Collection { get; }

    public async Task<ICollection<TDocument>> FilterByAsync(
        Expression<Func<TDocument, bool>> filterExpression)
    {
        return await Collection.Find(filterExpression).ToListAsync();
    }

    public async Task<ICollection<TProjected>> FilterByAsync<TProjected>(
        Expression<Func<TDocument, bool>> filterExpression,
        Expression<Func<TDocument, TProjected>> projectionExpression)
    {
        return await Collection
                     .Find(filterExpression)
                     .Project(projectionExpression)
                     .ToListAsync();
    }

    public async Task<TDocument> FindOneAsync(
        Expression<Func<TDocument, bool>> filterExpression,
        CancellationToken cancellationToken)
    {
        return await Collection
                     .Find(filterExpression)
                     .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TProjected> FindOneAsync<TProjected>(
        Expression<Func<TDocument, bool>> filterExpression,
        Expression<Func<TDocument, TProjected>> projectionExpression,
        CancellationToken cancellationToken)
    {
        return await Collection
                     .Find(filterExpression)
                     .Project(projectionExpression)
                     .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task InsertOneAsync(TDocument document, CancellationToken cancellationToken)
    {
        var insertOptions = new InsertOneOptions
        {
            BypassDocumentValidation = false
        };

        await Collection.InsertOneAsync(document, insertOptions, cancellationToken);
    }

    public async Task InsertManyAsync(ICollection<TDocument> documents)
    {
        await Collection.InsertManyAsync(documents);
    }

    public async Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression)
    {
        await Collection.FindOneAndDeleteAsync(filterExpression);
    }

    public async Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression)
    {
        await Collection.DeleteManyAsync(filterExpression);
    }
}