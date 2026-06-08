using System.Linq.Expressions;
using Blog.Identity.Domain.Entities.Common;

namespace Blog.Identity.Application.Contracts.Persistence.Common;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task        CreateAsync(T entity, CancellationToken ct = default);
    Task<T?>    GetByAsync(Expression<Func<T,bool>> predicate, CancellationToken ct = default, bool withTracking = false);
    Task<bool>  AnyAsync(Expression<Func<T,bool>> predicate, CancellationToken ct = default);
    void        Update(T entity);
    void        Delete(T entity);
}
