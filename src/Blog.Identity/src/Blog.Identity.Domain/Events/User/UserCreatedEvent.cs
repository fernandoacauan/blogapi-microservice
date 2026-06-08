using Blog.Identity.Domain.Entities.Common;

namespace Blog.Identity.Domain.Events.User;

public sealed record class UserCreatedEvent(Guid Id, string Name, string Surname, string Email) : IDomainEvent;
