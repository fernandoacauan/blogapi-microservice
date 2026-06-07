using Blog.Identity.Application.Contracts.Persistence.User;
using Blog.Identity.Application.DTOs.User;
using Blog.Identity.Application.Exceptions;
using MediatR;

namespace Blog.Identity.Application.Features.User.Queries.GetUserByIdQuery;

public sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
{
    private readonly IUserQuery _userQuery;

    public GetUserByIdQueryHandler(IUserQuery userQuery)
    {
        _userQuery = userQuery;
    }

    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        return await _userQuery.GetUserById(request.Id, cancellationToken) 
            ?? throw new NotFoundException("User not found");
    }
}
