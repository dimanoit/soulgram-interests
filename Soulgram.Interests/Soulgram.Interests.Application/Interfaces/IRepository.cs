using System.Linq.Expressions;

namespace Soulgram.Interests.Application.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    Task<ICollection<TEntity>> FilterByAsync(Expression<Func<TEntity, bool>> filterExpression);

    Task<ICollection<TProjected>> FilterByAsync<TProjected>(
        Expression<Func<TEntity, bool>> filterExpression,
        Expression<Func<TEntity, TProjected>> projectionExpression);

    Task<TEntity> FindOneAsync(
        Expression<Func<TEntity, bool>> filterExpression,
        CancellationToken cancellationToken);

    Task<TProjected> FindOneAsync<TProjected>(
        Expression<Func<TEntity, bool>> filterExpression,
        Expression<Func<TEntity, TProjected>> projectionExpression,
        CancellationToken cancellationToken);

    Task InsertOneAsync(TEntity document, CancellationToken cancellationToken);
    Task InsertManyAsync(ICollection<TEntity> documents);
    Task DeleteOneAsync(Expression<Func<TEntity, bool>> filterExpression);
    Task DeleteManyAsync(Expression<Func<TEntity, bool>> filterExpression);
}