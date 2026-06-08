namespace Blog.Identity.Application.DTOs.User;

public sealed class UserLoginDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string HashedPassword { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsAdmin { get; set; } = false;
}
