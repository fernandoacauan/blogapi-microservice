using Blog.Identity.Application.Features.Role.Common;
using MediatR;

namespace Blog.Identity.Application.Features.Role.Commands.UpdateRoleById;

public sealed class UpdateRoleByIdCommand(string name, bool isAdmin) : BaseCommand(name, isAdmin), IRequest<Unit>
{
    public Guid Id { get; set; }
}
