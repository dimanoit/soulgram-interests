using System.Linq.Expressions;

namespace Soulgram.Interests.Application.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    Task<ICollection<TProjected>> FilterByAsync<TProjected>(
        Expression<Func<TEntity, bool>> filterExpression,
        Expression<Func<TEntity, TProjected>> projectionExpression);

    Task<TProjected> FindOneAsync<TProjected>(
        Expression<Func<TEntity, bool>> filterExpression,
        Expression<Func<TEntity, TProjected>> projectionExpression,
        CancellationToken cancellationToken);

    Task InsertOneAsync(TEntity document, CancellationToken cancellationToken);
    Task InsertManyAsync(ICollection<TEntity> documents, CancellationToken cancellationToken);
    Task DeleteOneAsync(Expression<Func<TEntity, bool>> filterExpression);
    Task DeleteManyAsync(Expression<Func<TEntity, bool>> filterExpression);
}