using System;
using Blog.SharedKernel.Contracts;
using Blog.Solution.Application.Features.Author.Commands.CreateAuthorCommand;
using MassTransit;
using MediatR;

namespace Blog.Solution.Application.Consumers.Author;

public sealed class AuthorCreatedConsumer : IConsumer<IUserCreatedEvent>
{
    private readonly IMediator _mediator;

    public AuthorCreatedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<IUserCreatedEvent> context)
    {
        var author = context.Message;

        await _mediator.Send(new CreateAuthorCommand(author.Id, author.Name, author.Surname, author.Email), context.CancellationToken);
    }
}
