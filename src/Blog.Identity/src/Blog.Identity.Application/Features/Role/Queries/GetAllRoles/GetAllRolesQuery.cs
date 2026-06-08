using Blog.Identity.Application.DTOs.Role;
using MediatR;

namespace Blog.Identity.Application.Features.Role.Queries.GetAllRoles;

public sealed record class GetAllRolesQuery : IRequest<IReadOnlyList<RoleDto>>;
