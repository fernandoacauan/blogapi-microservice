using System;
using System.Linq.Expressions;
using Blog.Solution.Domain.Entities.Common;

namespace Blog.Solution.Application.Contracts.Persistence.Common;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<bool>  AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default);
    Task<T?>    GetByAsync(Expression<Func<T,bool>> predicate, CancellationToken ct = default, bool withTracking = false);
    Task        CreateAsync(T entity, CancellationToken ct = default);
    void        Update(T entity);
    void        Delete(T entity);
}
