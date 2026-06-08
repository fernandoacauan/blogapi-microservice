namespace Blog.Identity.Application.Features.User.Common;

public class BaseCommand
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    protected BaseCommand()
    {
    }

    protected BaseCommand(string name, string surname, string email, string password)
    {
        Name = name;
        Surname = surname;
        Email = email;
        Password = password;
    }
}
