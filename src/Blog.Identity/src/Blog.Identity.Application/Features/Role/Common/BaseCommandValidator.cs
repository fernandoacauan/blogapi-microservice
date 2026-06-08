using FluentValidation;

namespace Blog.Identity.Application.Features.Role.Common;

public sealed class BaseCommandValidator : AbstractValidator<BaseCommand>
{
    public BaseCommandValidator()
    {
        RuleFor(r => r.Name).MaximumLength(50)
                            .NotNull()
                            .NotEmpty();
    }
}
