using Blog.Identity.Application.Contracts.Infrastructure.Publisher;
using MassTransit;

namespace Blog.Identity.Infrastructure.Implementations.Publisher;

public sealed class MessagePublisher<T> : IMessagePublisher<T> where T : class
{
    private readonly IPublishEndpoint _publishEndpoint;

    public MessagePublisher(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task PublishMessageAsync(object message, CancellationToken ct = default)
    {
        await _publishEndpoint.Publish<T>(message!, ct);
    }
}
