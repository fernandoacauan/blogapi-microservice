using System.Data;
using Blog.Identity.Application.Contracts.Persistence.Common;
using Blog.Identity.Application.Contracts.Persistence.Role;
using Blog.Identity.Application.Contracts.Persistence.User;
using Blog.Identity.Persistence.Configurations.DbSettings;
using Blog.Identity.Persistence.Context;
using Blog.Identity.Persistence.Repository.Common;
using Blog.Identity.Persistence.Repository.Role;
using Blog.Identity.Persistence.Repository.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Blog.Identity.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceService(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<IdentityDbSettings>(config.GetSection("IdentityDbSettings"));
        services.AddDbContext<AuthDbContext>( (provider, op) =>
        {
            var con = provider.GetRequiredService<IOptions<IdentityDbSettings>>().Value;

            if (con == null  || string.IsNullOrEmpty(con.ConnectionString))
            {
                throw new InvalidOperationException("Missing connection string");
            }
            op.UseNpgsql(con.ConnectionString);

        });
    
        services.AddScoped<IDbConnection>(provider => provider.GetRequiredService<AuthDbContext>().Database.GetDbConnection());
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserQuery, UserQuery>();
        services.AddScoped<IRoleQuery, RoleQuery>();
        return services;
    }
}
