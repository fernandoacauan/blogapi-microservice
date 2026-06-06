using BCryptNet;
using Blog.Identity.Application.Abstractions.Password;

namespace Blog.Identity.Infrastructure.Implementations.Password;

internal sealed class PasswordService : IPasswordService
{
    public string HashPassword(string password)
    {
        return BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Verify(password, hashedPassword);
    }
}
