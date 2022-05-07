using System.Linq.Expressions;

namespace Soulgram.Interests.Application;

public interface IMongoRepository<TDocument> where TDocument : class
{
    Task<ICollection<TDocument>> FilterByAsync(Expression<Func<TDocument, bool>> filterExpression);

    Task<ICollection<TProjected>> FilterByAsync<TProjected>(
        Expression<Func<TDocument, bool>> filterExpression,
        Expression<Func<TDocument, TProjected>> projectionExpression);

    Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression);
    Task InsertOneAsync(TDocument document);
    Task InsertManyAsync(ICollection<TDocument> documents);
    Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression);
    Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression);
}