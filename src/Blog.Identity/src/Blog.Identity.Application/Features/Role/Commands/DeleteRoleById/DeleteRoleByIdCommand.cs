using MediatR;

namespace Blog.Identity.Application.Features.Role.Commands.DeleteRoleById;

public sealed record class DeleteRoleByIdCommand(Guid Id) : IRequest<Unit>;
