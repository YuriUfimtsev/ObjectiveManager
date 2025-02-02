using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace ObjectiveManager.Models.EntityFramework;

public class CrudRepository<TKey, TEntity> : ReadOnlyRepository<TKey, TEntity>, ICrudRepository<TKey, TEntity>
    where TKey : IEquatable<TKey>
    where TEntity : class, IEntity<TKey>
{
    protected CrudRepository(DbContext context)
        : base(context)
    {
    }

    public async Task<TKey> AddAsync(TEntity item)
    {
        await Context.AddAsync(item);
        await Context.SaveChangesAsync();
        return item.Id;
    }

    public async Task<int> DeleteAsync(TKey id) 
        => await Context.Set<TEntity>()
            .Where(entity => entity.Id.Equals(id))
            .DeleteAsync();

    public async Task<int> UpdateAsync(TKey id, Expression<Func<TEntity, TEntity>> updateFactory) 
        => await Context.Set<TEntity>()
            .Where(entity => entity.Id.Equals(id))
            .UpdateAsync(updateFactory);
}