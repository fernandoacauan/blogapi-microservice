using System;
using Blog.Identity.Application.Features.User.Common;
using FluentValidation;

namespace Blog.Identity.Application.Features.User.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        Include(new BaseCommandValidator());
    }
}
