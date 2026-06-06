using Blog.Identity.Application.Abstractions.Password;
using Blog.Identity.Infrastructure.Configurations.Jwt;
using Blog.Identity.Infrastructure.Implementations.Password;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Identity.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<JwtSettings>(config.GetSection("JwtSettings"));
        services.AddJwtService(config);

        services.AddScoped<IPasswordService, PasswordService>();
        return services;
    }
}
