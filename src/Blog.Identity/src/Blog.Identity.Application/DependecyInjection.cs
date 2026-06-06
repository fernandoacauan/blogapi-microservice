using System.Reflection;
using Blog.Identity.Application.Behavior.Validator;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Identity.Application;

public static class DependecyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        Assembly asm = Assembly.GetExecutingAssembly();

        services.AddValidatorsFromAssembly(asm);
        services.AddMediatR(cfg =>
        {
            cfg.AddOpenBehavior(typeof(ValidatorBehavior<,>));
            cfg.RegisterServicesFromAssembly(asm);
        });
        return services;
    }
}
