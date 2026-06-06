using Blog.Identity.Application.Features.Role.Common;
using FluentValidation;

namespace Blog.Identity.Application.Features.Role.Commands.UpdateRoleById;

public sealed class UpdateRoleByIdCommandValidator : AbstractValidator<UpdateRoleByIdCommand>
{
    public UpdateRoleByIdCommandValidator()
    {
        Include(new BaseCommandValidator());
    }
}
