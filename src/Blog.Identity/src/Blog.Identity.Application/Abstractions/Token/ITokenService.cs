using Blog.Identity.Application.DTOs.User;
using Blog.Identity.Domain.Entities.User;

namespace Blog.Identity.Application.Abstractions.Token;

public interface ITokenService
{
    string GenerateToken(UserLoginDto userLoginDto);
}
