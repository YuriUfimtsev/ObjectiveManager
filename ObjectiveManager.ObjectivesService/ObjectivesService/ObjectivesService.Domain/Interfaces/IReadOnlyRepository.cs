using System.Linq.Expressions;
using ObjectivesService.Domain.Entities;

namespace ObjectivesService.Domain.Interfaces;

public interface IReadOnlyRepository<TKey, TEntity>
    where TKey : IEquatable<TKey>
    where TEntity : IEntity<TKey>
{
    public IQueryable<TEntity> GetAll();
    
    public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate);
    
    public Task<TEntity?> GetAsync(TKey id);
    
    public Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate);
}