namespace Blog.Solution.Infrastructure.Configurations.MessageBroker.RabbitMQ;

public sealed class RabbitMqSettings
{
    public required string User { get; set; }
    public required string Pass { get; set; }
    public required string Host { get; set; }
}
