namespace Blog.Solution.Application.Features.Author.Common;

public class BaseCommand
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    protected BaseCommand()
    {
        
    }

    public BaseCommand(string name, string surname, string email)
    {
        Name = name;
        Surname = surname;
        Email = email;
    }
}
