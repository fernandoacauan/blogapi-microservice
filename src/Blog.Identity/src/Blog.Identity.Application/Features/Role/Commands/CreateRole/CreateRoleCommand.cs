using Blog.Identity.Application.Features.Role.Common;
using MediatR;

namespace Blog.Identity.Application.Features.Role.Commands.CreateRole;

public sealed class CreateRoleCommand(string name, bool isAdmin) : BaseCommand(name, isAdmin), IRequest<Guid>;
