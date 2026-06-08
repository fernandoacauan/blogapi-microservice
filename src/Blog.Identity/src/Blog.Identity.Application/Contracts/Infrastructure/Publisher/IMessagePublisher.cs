namespace Blog.Identity.Application.Contracts.Infrastructure.Publisher;

public interface IMessagePublisher<T>
{
    Task    PublishMessageAsync(object message, CancellationToken ct = default);
}
