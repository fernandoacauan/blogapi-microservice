using Blog.Identity.Application.Contracts.Persistence.Common;
using Blog.Identity.Application.Contracts.Persistence.Role;
using Blog.Identity.Application.Contracts.Persistence.User;
using Blog.Identity.Persistence.Context;
using Blog.Identity.Persistence.Repository.Role;
using Blog.Identity.Persistence.Repository.User;

namespace Blog.Identity.Persistence.Repository.Common;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly AuthDbContext _context;
    private Lazy<IUserRepository> _user;
    private Lazy<IRoleRepository> _role;

    public IUserRepository User => _user.Value;

    public IRoleRepository Role => _role.Value;

    public UnitOfWork(AuthDbContext context)
    {
        _context = context;
        _user = new(() => new UserRepository(context));
        _role = new(() => new RoleRepository(context));
    }


    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        await _context.SaveChangesAsync(ct);
    }
}
