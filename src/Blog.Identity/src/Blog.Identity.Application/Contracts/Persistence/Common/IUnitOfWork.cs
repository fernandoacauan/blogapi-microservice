using Blog.Identity.Application.Contracts.Persistence.Role;
using Blog.Identity.Application.Contracts.Persistence.User;

namespace Blog.Identity.Application.Contracts.Persistence.Common;

public interface IUnitOfWork
{
    IUserRepository User { get; }
    IRoleRepository Role { get; }

    Task        SaveChangesAsync(CancellationToken ct = default);
}
