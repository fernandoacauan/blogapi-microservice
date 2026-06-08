using System.Data;
using Blog.Solution.Application.Contracts.Persistence.Common;
using Blog.Solution.Persistence.Configurations.DbSettings;
using Blog.Solution.Persistence.Context.BlogDbContext;
using Blog.Solution.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Blog.Solution.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceService(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<BlogDbSettings>(config.GetSection("BlogDbSettings"));

        services.AddDbContext<BlogDbContext>((provider, op) =>
        {
            var con = provider.GetRequiredService<IOptions<BlogDbSettings>>().Value;

            if (con == null || string.IsNullOrEmpty(con.ConnectionString))
            {
                throw new InvalidOperationException("Missing connection string");
            }

            op.UseNpgsql(con.ConnectionString);
        });

        services.AddScoped<IDbConnection>(provider => provider.GetRequiredService<BlogDbContext>().Database.GetDbConnection());
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}
