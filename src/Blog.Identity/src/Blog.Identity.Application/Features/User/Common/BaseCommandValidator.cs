using FluentValidation;

namespace Blog.Identity.Application.Features.User.Common;

public sealed class BaseCommandValidator : AbstractValidator<BaseCommand>
{
    public BaseCommandValidator()
    {
        RuleFor(u => u.Name).MaximumLength(80)
                            .NotNull()
                            .NotEmpty();

        RuleFor(u => u.Surname).MaximumLength(100)
                                .NotNull()
                                .NotEmpty();

        RuleFor(u => u.Email).MaximumLength(255)
                            .NotNull()
                            .NotEmpty()
                            .EmailAddress();

        RuleFor(u => u.Password).MaximumLength(255)
                                        .NotNull()
                                        .NotEmpty();
    }
}
