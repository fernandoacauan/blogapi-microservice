using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Solution.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        Assembly asm = Assembly.GetExecutingAssembly();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(asm);
        });
        return services;
    }
}
