using System;
using MediatR;

namespace Blog.Identity.Application.Abstractions.Event;

public interface IEventPublisher
{
    Task        PublishEvent(INotification domainEvent, CancellationToken ct);
}
