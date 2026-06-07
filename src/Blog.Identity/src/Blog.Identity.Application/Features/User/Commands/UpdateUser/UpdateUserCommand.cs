using Blog.Identity.Application.Features.User.Common;
using MediatR;

namespace Blog.Identity.Application.Features.User.Commands.UpdateUser;

public sealed class UpdateUserCommand : BaseCommand, IRequest<Unit>
{
    public Guid Id { get; set; }
}
