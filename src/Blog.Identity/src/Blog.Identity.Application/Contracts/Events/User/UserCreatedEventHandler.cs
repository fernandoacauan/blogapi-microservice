using Blog.Identity.Domain.Events.User;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Blog.Identity.Application.Contracts.Events.User;

sealed internal class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
{
    private ILogger<UserCreatedEvent> _logger;

    public UserCreatedEventHandler(ILogger<UserCreatedEvent> logger)
    {
        _logger = logger;
    }

    public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Welcome {email}", notification.Email);

        return Task.CompletedTask;
    }
}
