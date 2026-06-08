namespace Blog.Identity.Application.Abstractions.Password;

public interface IPasswordService
{
    string  HashPassword(string password);
    bool    VerifyPassword(string password, string hashedPassword);
}
