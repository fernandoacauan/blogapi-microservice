using Blog.Identity.Domain.Entities.Common;

namespace Blog.Identity.Domain.Events.User;

public sealed record class UserCreatedEvent(string Name, string Email) : IDomainEvent;
