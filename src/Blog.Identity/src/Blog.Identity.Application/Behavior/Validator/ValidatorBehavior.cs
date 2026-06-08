using FluentValidation;
using MediatR;

namespace Blog.Identity.Application.Behavior.Validator;

public sealed class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidatorBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var validations = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(request, cancellationToken)));
            var failures = validations.Where(v => !v.IsValid).SelectMany(v => v.Errors).ToList();

            if (failures.Count != 0)
            {
                throw new ValidationException(failures);
            }
        }
    
        return await next();
    }
}
