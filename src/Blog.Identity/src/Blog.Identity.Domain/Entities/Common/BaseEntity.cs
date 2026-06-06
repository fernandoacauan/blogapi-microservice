namespace Blog.Identity.Domain.Entities.Common;

public abstract class BaseEntity
{
    public Guid Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; protected set; } = DateTime.UtcNow;
    private readonly List<IDomainEvent> _events = new();

    public IReadOnlyList<IDomainEvent> Events => _events.AsReadOnly();

    public void AddEvent(IDomainEvent events) => _events.Add(events);
    public void Clear() => _events.Clear();

    protected BaseEntity(Guid id)
    {
        Id = id;
    }

    protected BaseEntity(Guid id, DateTime createdAt, DateTime updatedAt)
    {
        Id = id;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    protected BaseEntity()
    {
        Id = Guid.NewGuid();
    }
}
