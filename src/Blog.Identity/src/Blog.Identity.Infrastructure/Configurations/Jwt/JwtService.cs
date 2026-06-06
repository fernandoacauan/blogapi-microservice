using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Blog.Identity.Infrastructure.Configurations.Jwt;

public static class JwtServices
{
    public static IServiceCollection AddJwtService(this IServiceCollection services, IConfiguration config)
    {
        var settings = config.GetSection("JwtSettings").Get<JwtSettings>() ?? throw new InvalidOperationException("Missing JWT settings");

        services.AddAuthorization(cfg =>
        {
            cfg.AddPolicy("IsAdmin", p => p.RequireClaim("Admin", "true"));
        })
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(o =>
        {
            o.RequireHttpsMetadata = true;
            o.SaveToken = true;
            o.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = settings.Issuer,
                ValidAudience = settings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Key))
            };
        });
        return services;
    }
}
