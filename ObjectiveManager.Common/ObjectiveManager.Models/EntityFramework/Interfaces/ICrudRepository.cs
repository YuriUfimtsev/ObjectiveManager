using System.Linq.Expressions;

namespace ObjectiveManager.Models.EntityFramework;

public interface ICrudRepository<TKey, TEntity> : IReadOnlyRepository<TKey, TEntity>
    where TKey : IEquatable<TKey>
    where TEntity : IEntity<TKey>
{
    public Task<TKey> AddAsync(TEntity item);
    
    public Task<int> DeleteAsync(TKey id);
    
    public Task<int> UpdateAsync(TKey id, Expression<Func<TEntity, TEntity>> updateFactory);
}
