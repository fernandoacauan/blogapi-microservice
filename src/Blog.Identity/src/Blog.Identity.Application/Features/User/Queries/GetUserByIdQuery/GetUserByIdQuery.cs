using Blog.Identity.Application.DTOs.User;
using MediatR;

namespace Blog.Identity.Application.Features.User.Queries.GetUserByIdQuery;

public sealed record class GetUserByIdQuery(Guid Id) : IRequest<UserDto>;
