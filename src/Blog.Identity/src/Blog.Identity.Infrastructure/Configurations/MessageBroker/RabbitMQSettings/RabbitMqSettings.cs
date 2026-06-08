
namespace Blog.Identity.Infrastructure.Configurations.MessageBroker.RabbitMQSettings;

public sealed class RabbitMqSettings
{
    public required string Name { get; set; }
    public required string Pass { get; set; }
    public required string Host { get; set; }
}
