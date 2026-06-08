using Blog.Identity.Application.Features.User.Common;
using MediatR;

namespace Blog.Identity.Application.Features.User.Commands.CreateUser;

public sealed class CreateUserCommand : BaseCommand, IRequest<Guid>
{
    public CreateUserCommand()
    {
    }

    public CreateUserCommand(string name, string surname, string email, string password) : base(name, surname, email, password)
    {
    }
}
