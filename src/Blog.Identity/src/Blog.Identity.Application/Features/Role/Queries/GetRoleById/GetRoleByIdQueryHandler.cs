using Blog.Identity.Application.Contracts.Persistence.Role;
using Blog.Identity.Application.DTOs.Role;
using Blog.Identity.Application.Exceptions;
using MediatR;

namespace Blog.Identity.Application.Features.Role.Queries.GetRoleById;

public sealed class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, RoleDto>
{
    private readonly IRoleQuery _query;

    public GetRoleByIdQueryHandler(IRoleQuery query)
    {
        _query = query;
    }

    public async Task<RoleDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
       return await _query.GetByIdAsync(request.Id, cancellationToken) ?? throw new NotFoundException("Role not found");
    }
}
