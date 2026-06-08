namespace Blog.SharedKernel.Contracts;

public interface IUserCreatedEvent
{
    Guid Id { get; }
    string Name { get; }
    string Surname { get; }
    string Email { get; }
}
