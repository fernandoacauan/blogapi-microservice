using System.Security.Claims;
using Blog.Identity.Application.Abstractions.TokenInfo;
using Microsoft.AspNetCore.Http;

namespace Blog.Identity.Infrastructure.Implementations.TokenInfo;

internal sealed class TokenInformation : ITokenInformation
{
    private readonly IHttpContextAccessor _httpContext;

    public TokenInformation(IHttpContextAccessor httpContext)
    {
        _httpContext = httpContext;
    }

    public string GetEmail()
    {
        string email = _httpContext.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value 
            ?? throw new UnauthorizedAccessException("Missing email");

        return email;
    }

    public Guid GetId()
    {
        Guid id = Guid.Parse(_httpContext.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? throw new UnauthorizedAccessException("Missing email"));

        return id;
    }

    public bool IsAdmin()
    {
        bool isAdmin = Convert.ToBoolean(_httpContext.HttpContext?.User?.FindFirst("Admin")?.Value
            ?? throw new UnauthorizedAccessException("Missing email"));

        return isAdmin;
    }
}
