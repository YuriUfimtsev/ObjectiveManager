using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ObjectiveManager.Domain.Entities;
using ObjectiveManager.Domain.Interfaces;
using Z.EntityFramework.Plus;

namespace ObjectiveManager.DataAccess.Repositories;

public class CrudRepository<TKey, TEntity> : ReadOnlyRepository<TKey, TEntity>, ICrudRepository<TKey, TEntity>
    where TKey : IEquatable<TKey>
    where TEntity : class, IEntity<TKey>
{
    public CrudRepository(DbContext context)
        : base(context)
    {
    }

    public async Task<TKey> AddAsync(TEntity item)
    {
        await Context.AddAsync(item);
        await Context.SaveChangesAsync();
        return item.Id;
    }

    public async Task DeleteAsync(TKey id)
    {
        await Context.Set<TEntity>()
            .Where(entity => entity.Id.Equals(id))
            .DeleteAsync();
    }

    public async Task UpdateAsync(TKey id, Expression<Func<TEntity, TEntity>> updateFactory)
    {
        await Context.Set<TEntity>()
            .Where(entity => entity.Id.Equals(id))
            .UpdateAsync(updateFactory);
    }
}