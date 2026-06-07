using MediatR;

namespace Blog.Identity.Application.Features.User.Queries.Login;

public sealed record class LoginQuery(string Email, string Password) : IRequest<string>;
