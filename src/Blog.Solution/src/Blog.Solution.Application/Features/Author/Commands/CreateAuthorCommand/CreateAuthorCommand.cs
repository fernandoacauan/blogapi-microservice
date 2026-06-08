using System;
using Blog.Solution.Application.Features.Author.Common;
using MediatR;

namespace Blog.Solution.Application.Features.Author.Commands.CreateAuthorCommand;

public sealed class CreateAuthorCommand : BaseCommand, IRequest<Guid>
{
    public Guid Id { get; set; }

    public CreateAuthorCommand(Guid id, string name, string surname, string email) : base(name, surname, email)
    {
        Id = id;
    }
}
