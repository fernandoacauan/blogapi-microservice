using Blog.Identity.Application.DTOs.Role;
using MediatR;

namespace Blog.Identity.Application.Features.Role.Queries.GetRoleById;

public sealed record class GetRoleByIdQuery(Guid Id) : IRequest<RoleDto>;
