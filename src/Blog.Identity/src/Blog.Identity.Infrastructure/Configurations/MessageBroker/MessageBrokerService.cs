using Blog.Identity.Infrastructure.Configurations.MessageBroker.RabbitMQSettings;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Identity.Infrastructure.Configurations.MessageBroker;

public static class MessageBrokerService
{
    public static IServiceCollection AddMessageBrokerService(this IServiceCollection services, IConfiguration config)
    {
        var settings = config.GetSection("RabbitMq").Get<RabbitMqSettings>()
            ?? throw new InvalidOperationException("Missing RabbitMQ Settings");

        services.AddMassTransit(cfg =>
        {
            cfg.UsingRabbitMq((context, x) =>
            {
                x.Host(settings.Host, "/", h =>
                {
                    h.Username(settings.Name);
                    h.Password(settings.Pass);
                });
                x.ConfigureEndpoints(context);
            });
        });
        return services;
    }
}
