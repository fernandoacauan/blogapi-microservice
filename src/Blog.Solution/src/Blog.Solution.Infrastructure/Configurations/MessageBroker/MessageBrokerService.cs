using System;
using Blog.Solution.Application.Consumers.Author;
using Blog.Solution.Infrastructure.Configurations.MessageBroker.RabbitMQ;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Solution.Infrastructure.Configurations.MessageBroker;

public static class MessageBrokerService
{
    public static IServiceCollection AddMessageBrokerService(this IServiceCollection services, IConfiguration config)
    {
        var settings = config.GetSection("RabbitMq").Get<RabbitMqSettings>() 
            ?? throw new InvalidOperationException("RabbitMQ Settings not found");

        services.AddMassTransit(cfg =>
        {
            cfg.UsingRabbitMq((context, x) =>
            {
                x.Host(settings.Host, "/", h =>
                {
                    h.Username(settings.User);
                    h.Password(settings.Pass);
                });

                x.ConfigureEndpoints(context);
            });

            cfg.AddConsumer<AuthorCreatedConsumer>();
        });

        return services;
    }
}
