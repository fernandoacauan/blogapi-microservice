using Blog.Identity.Application.Features.User.Common;
using FluentValidation;

namespace Blog.Identity.Application.Features.User.Commands.CreateUser;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        Include(new BaseCommandValidator());
    }
}
