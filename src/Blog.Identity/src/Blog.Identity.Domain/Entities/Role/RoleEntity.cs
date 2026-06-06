using Blog.Identity.Domain.Entities.Common;

namespace Blog.Identity.Domain.Entities.Role;

public sealed class RoleEntity : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public bool IsAdmin { get; private set; } = false;

    private RoleEntity()
    {
        
    }

    public RoleEntity(string name, bool isAdmin)
    {
        Name = name;
        IsAdmin = isAdmin;
    }

    public void SetName(string name) => Name = name;
    public void SetAdmin(bool isAdmin) => IsAdmin = isAdmin;
}
