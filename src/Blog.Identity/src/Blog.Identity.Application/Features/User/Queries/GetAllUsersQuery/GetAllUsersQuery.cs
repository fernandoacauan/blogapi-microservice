using Blog.Identity.Application.DTOs.User;
using MediatR;

namespace Blog.Identity.Application.Features.User.Queries.GetAllUsersQuery;

public sealed record class GetAllUsersQuery : IRequest<IReadOnlyList<UserDto>>;
