using Blog.Identity.Application.Abstractions.Event;
using MediatR;

namespace Blog.Identity.Infrastructure.Implementations.Event;

internal class EventPublisher : IEventPublisher
{
    private readonly IMediator _mediator;

    public EventPublisher(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task PublishEvent(INotification domainEvent, CancellationToken ct)
    {
        await _mediator.Publish(domainEvent, ct);
    }
}
