using Blog.Identity.Domain.Entities.Common;
using Blog.Identity.Domain.Entities.Role;
using Blog.Identity.Domain.Events.User;

namespace Blog.Identity.Domain.Entities.User;

public sealed class UserEntity : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Surname { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string HashedPassword { get; private set; } = string.Empty;
    public Guid RoleId { get; private set; }
    public RoleEntity Role { get; private set; } = default!;
    public void SetHashedPassword(string hashedPassword) => HashedPassword = hashedPassword;

    public UserEntity(string name, string surname, string email, string hashedPassword, Guid roleId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(surname);
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        ArgumentException.ThrowIfNullOrWhiteSpace(hashedPassword);

        Name = name;
        Surname = surname;
        Email = email;
        HashedPassword = hashedPassword;
        RoleId = roleId;

        AddEvent(new UserCreatedEvent(Id, Name, Surname, Email));
    }

    private UserEntity()
    {
        
    }

}
