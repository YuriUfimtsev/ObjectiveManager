using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ObjectivesService.Domain.Entities;
using ObjectivesService.Domain.Interfaces;

namespace ObjectivesService.DataAccess.Repositories;

public class ReadOnlyRepository<TKey, TEntity> : IReadOnlyRepository<TKey, TEntity>
    where TKey : IEquatable<TKey>
    where TEntity : class, IEntity<TKey>
{
    protected readonly DbContext Context;

    public ReadOnlyRepository(DbContext context)
    {
        Context = context;
    }

    public IQueryable<TEntity> GetAll()
        => Context.Set<TEntity>().AsNoTracking();

    public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate)
        => Context.Set<TEntity>().AsNoTracking().Where(predicate);

    public async Task<TEntity?> GetAsync(TKey id)
        => await Context.FindAsync<TEntity>(id);

    public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate)
        => await Context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(predicate);
}
