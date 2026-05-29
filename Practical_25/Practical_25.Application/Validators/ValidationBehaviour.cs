using FluentValidation;
using MediatR;

namespace Practical_25.Application.Validators;

public class ValidationBehaviour<TRequest,TResponse> : IPipelineBehavior<TRequest,TResponse> where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validate;
    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validate)
    {
        _validate = validate;
    }
    public async Task<TResponse> Handle(TRequest request,RequestHandlerDelegate<TResponse> next,CancellationToken token)
    {
        var context = new ValidationContext<TRequest>(request);

        var failures = _validate.Select(x => x.Validate(context)).SelectMany(x => x.Errors).Where(x => x != null).ToList();
        if (failures.Any())
            throw new ValidationException(failures);
        return await next();
    }
}

