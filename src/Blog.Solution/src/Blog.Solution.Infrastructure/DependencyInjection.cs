using Blog.Solution.Infrastructure.Configurations.MessageBroker;
using Blog.Solution.Infrastructure.Configurations.MessageBroker.RabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Solution.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<RabbitMqSettings>(config.GetSection("RabbitMq"));

        services.AddMessageBrokerService(config);
        return services;
    }
}
