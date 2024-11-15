using System.Linq.Expressions;
using ObjectiveManager.Domain.Entities;

namespace ObjectiveManager.Domain.Interfaces;

public interface ICrudRepository<TKey, TEntity> : IReadOnlyRepository<TKey, TEntity>
    where TKey : IEquatable<TKey>
    where TEntity : IEntity<TKey>
{
    public Task<TKey> AddAsync(TEntity item);
    
    public Task DeleteAsync(TKey id);
    
    public Task UpdateAsync(TKey id, Expression<Func<TEntity, TEntity>> updateFactory);
}
