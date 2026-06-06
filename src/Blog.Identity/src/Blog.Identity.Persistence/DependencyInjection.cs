using System.Data;
using Blog.Identity.Persistence.Configurations.DbSettings;
using Blog.Identity.Persistence.Context;
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
        return services;
    }
}
