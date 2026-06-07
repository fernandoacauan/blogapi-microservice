using System;
using MediatR;

namespace Blog.Identity.Application.Features.User.Commands.DeleteUser;

public sealed record class DeleteUserCommandById(Guid Id) : IRequest<Unit>;
