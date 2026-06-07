using Blog.Identity.Application.DTOs.User;

namespace Blog.Identity.Application.Contracts.Persistence.User;

public interface IUserQuery
{
    Task<UserDto?>                  GetUserById(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<UserDto>>    GetAllUsersAsync(CancellationToken ct = default);
    Task<UserLoginDto?>             GetLoginUserByEmailAsync(string email, CancellationToken ct = default);
}
