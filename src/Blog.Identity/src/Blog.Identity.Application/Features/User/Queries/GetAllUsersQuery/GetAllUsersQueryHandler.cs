using Blog.Identity.Application.Contracts.Persistence.User;
using Blog.Identity.Application.DTOs.User;
using MediatR;

namespace Blog.Identity.Application.Features.User.Queries.GetAllUsersQuery;

public sealed class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IReadOnlyList<UserDto>>
{
    private readonly IUserQuery _userQuery;

    public GetAllUsersQueryHandler(IUserQuery userQuery)
    {
        _userQuery = userQuery;
    }

    public async Task<IReadOnlyList<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        return await _userQuery.GetAllUsersAsync(cancellationToken);
    }
}
