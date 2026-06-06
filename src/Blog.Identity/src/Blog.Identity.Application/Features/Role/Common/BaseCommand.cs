namespace Blog.Identity.Application.Features.Role.Common;

public abstract class BaseCommand
{
    public string Name { get; set; } = string.Empty;
    public bool IsAdmin { get; set; } = false;

    protected BaseCommand()
    {
        
    }

    protected BaseCommand(string name, bool isAdmin)
    {
        Name = name;
        IsAdmin = isAdmin;
    }
}
