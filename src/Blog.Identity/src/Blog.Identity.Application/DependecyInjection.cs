using System.Reflection;
using Blog.Identity.Application.Behavior.Validator;
using Blog.Identity.Application.Mapping.User;
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
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<UserProfile>();
        });
        return services;
    }
}
