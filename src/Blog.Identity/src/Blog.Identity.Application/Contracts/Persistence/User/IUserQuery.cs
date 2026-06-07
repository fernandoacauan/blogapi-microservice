using Blog.Identity.Application.DTOs.User;

namespace Blog.Identity.Application.Contracts.Persistence.User;

public interface IUserQuery
{
    Task<UserLoginDto?>  GetLoginUserByEmailAsync(string email, CancellationToken ct = default);
}
