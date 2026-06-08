using Blog.Identity.Application.Contracts.Infrastructure.Publisher;
using Blog.Identity.Domain.Events.User;
using Blog.SharedKernel.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Blog.Identity.Application.Contracts.Events.User;

sealed internal class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
{
    private ILogger<UserCreatedEvent> _logger;
    private IMessagePublisher<IUserCreatedEvent> _publisher;

    public UserCreatedEventHandler(ILogger<UserCreatedEvent> logger, IMessagePublisher<IUserCreatedEvent> publisher)
    {
        _logger = logger;
        _publisher = publisher;
    }

    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Welcome {email}", notification.Email);

        await _publisher.PublishMessageAsync(new
        {
            Id = notification.Id,
            Name = notification.Name,
            Surname = notification.Surname,
            Email = notification.Email,
        }, cancellationToken);
    }
}
