using Blog.Identity.Application.Features.Role.Common;
using FluentValidation;

namespace Blog.Identity.Application.Features.Role.Commands.CreateRole;

public sealed class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        Include(new BaseCommandValidator());
    }
}
