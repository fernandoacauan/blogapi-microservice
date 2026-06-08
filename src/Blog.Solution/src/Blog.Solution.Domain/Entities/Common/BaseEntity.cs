using System;
using Blog.Solution.Domain.Events;

namespace Blog.Solution.Domain.Entities.Common;

public abstract class BaseEntity
{
    public Guid Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; protected set; } = DateTime.UtcNow;
    public IReadOnlyList<IDomainEvent> Events => _events.AsReadOnly();
    private readonly List<IDomainEvent> _events = new();

    public void AddEvent(IDomainEvent events) => _events.Add(events);
    public void Clear() => _events.Clear();


    protected BaseEntity()
    {
        Id = Guid.NewGuid();
    }

    protected BaseEntity(Guid id)
    {
        Id = id;
    }
}
