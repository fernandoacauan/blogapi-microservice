using Blog.Identity.Application.DTOs.Role;

namespace Blog.Identity.Application.Contracts.Persistence.Role;

public interface IRoleQuery
{
    Task<IReadOnlyList<RoleDto>>    GetAllAsync(CancellationToken ct = default);
    Task<RoleDto?>                  GetByIdAsync(Guid id, CancellationToken ct = default);
}
