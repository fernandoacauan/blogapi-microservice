using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Blog.Identity.Application.Abstractions.Token;
using Blog.Identity.Application.DTOs.User;
using Blog.Identity.Domain.Entities.User;
using Blog.Identity.Infrastructure.Configurations.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Blog.Identity.Infrastructure.Implementations.Token;

public sealed class TokenService : ITokenService
{
    private JwtSettings _settings;

    public TokenService(IOptions<JwtSettings> settings)
    {
        _settings = settings.Value;
    }

    public string GenerateToken(UserLoginDto userLoginDto)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        JwtSecurityTokenHandler tokenHandler;

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, userLoginDto.Id.ToString()),
            new Claim(ClaimTypes.Name, userLoginDto.Name),
            new Claim(ClaimTypes.Surname, userLoginDto.Surname),
            new Claim(ClaimTypes.Email, userLoginDto.Email),
            new Claim("Admin", userLoginDto.IsAdmin ? "true" : "false")
        };

        var token = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = _settings.Issuer,
            Audience = _settings.Audience,
            SigningCredentials = creds,
            Expires = DateTime.UtcNow.AddHours(1)
        };

        tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.WriteToken(tokenHandler.CreateToken(token));
    }
}
