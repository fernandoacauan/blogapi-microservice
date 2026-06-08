using Blog.Identity.Application.Contracts.Persistence.Role;
using Blog.Identity.Application.DTOs.Role;
using MediatR;

namespace Blog.Identity.Application.Features.Role.Queries.GetAllRoles;

public sealed class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, IReadOnlyList<RoleDto>>
{
    private readonly IRoleQuery _query;

    public GetAllRolesQueryHandler(IRoleQuery query)
    {
        _query = query;
    }

    public async Task<IReadOnlyList<RoleDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        return await _query.GetAllAsync(cancellationToken);
    }
}
