using System.Linq.Expressions;
using Blog.Identity.Application.Contracts.Persistence.Common;
using Blog.Identity.Domain.Entities.Common;
using Blog.Identity.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Blog.Identity.Persistence.Repository.Common;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected DbSet<T> Set { get; }

    public BaseRepository(AuthDbContext context)
    {
        Set = context.Set<T>();
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default)
    {
        return await Set.AsNoTracking().AnyAsync(predicate, ct);
    }

    public async Task CreateAsync(T entity, CancellationToken ct = default)
    {
        await Set.AddAsync(entity, ct);
    }

    public void Delete(T entity)
    {
        Set.Remove(entity);
    }

    public async Task<T?> GetByAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default, bool withTracking = false)
    {
        IQueryable<T> query = Set;

        if (!withTracking)
        {
            query = query.AsNoTracking();
        }

        return await query.FirstOrDefaultAsync(predicate, ct);
    }

    public void Update(T entity)
    {
        Set.Update(entity);
    }
}
