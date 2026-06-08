using Blog.Identity.Application.Abstractions.Event;
using Blog.Identity.Application.Contracts.Persistence.Common;
using Blog.Identity.Application.Contracts.Persistence.Role;
using Blog.Identity.Application.Contracts.Persistence.User;
using Blog.Identity.Domain.Entities.Common;
using Blog.Identity.Persistence.Context;
using Blog.Identity.Persistence.Repository.Role;
using Blog.Identity.Persistence.Repository.User;
using MediatR;

namespace Blog.Identity.Persistence.Repository.Common;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly AuthDbContext _context;
    private Lazy<IUserRepository> _user;
    private Lazy<IRoleRepository> _role;
    private readonly IEventPublisher _events;

    public IUserRepository User => _user.Value;

    public IRoleRepository Role => _role.Value;

    public UnitOfWork(AuthDbContext context, IEventPublisher events)
    {
        _context = context;
        _user = new(() => new UserRepository(context));
        _role = new(() => new RoleRepository(context));
        _events = events;
    }


    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        var entities = _context.ChangeTracker.Entries<BaseEntity>().Where(b => b.Entity.Events.Any()).ToList();
        var events = entities.SelectMany(e => e.Entity.Events).Cast<IDomainEvent>().ToList();

        foreach (var e in entities)
        {
            e.Entity.Clear();
        }

        await _context.SaveChangesAsync(ct);

        foreach (var e in events)
        {
            await _events.PublishEvent(e, ct);
        }
    }
}
