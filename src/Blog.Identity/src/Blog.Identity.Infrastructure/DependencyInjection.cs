using Blog.Identity.Application.Abstractions.Event;
using Blog.Identity.Application.Abstractions.Password;
using Blog.Identity.Application.Abstractions.Token;
using Blog.Identity.Application.Abstractions.TokenInfo;
using Blog.Identity.Infrastructure.Configurations.Jwt;
using Blog.Identity.Infrastructure.Implementations.Event;
using Blog.Identity.Infrastructure.Implementations.Password;
using Blog.Identity.Infrastructure.Implementations.Token;
using Blog.Identity.Infrastructure.Implementations.TokenInfo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Identity.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<JwtSettings>(config.GetSection("JwtSettings"));
        services.AddJwtService(config);

        services.AddHttpContextAccessor();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IEventPublisher, EventPublisher>();
        services.AddScoped<ITokenInformation, TokenInformation>();
        return services;
    }
}
