using System.Reflection;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Result;
using Result.Abstractions;
using Result.Errors;

namespace ToDoList.Application.Behaviours;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IResult
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            validators.Select(v =>
                v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .Where(r => r.Errors.Count != 0)
            .SelectMany(r => r.Errors)
            .DistinctBy(x => new { x.PropertyName, x.ErrorCode })
            .ToList();

        if (failures.Count == 0)
        {
            return await next();
        }

        return GenerateFailedResult(failures);
    }

    private static TResponse GenerateFailedResult(IEnumerable<ValidationFailure> failures)
    {
        Type[] parameterTypes = [typeof(IEnumerable<Error>)];

        var factoryMethodInfo = typeof(TResponse).GetMethod(
            nameof(Result<Unit>.Failed),
            BindingFlags.Static | BindingFlags.Public,
            parameterTypes);

        if (factoryMethodInfo == null)
        {
            throw new Exception("Can not find factory method");
        }

        var errors = failures.Select(x => Error.Validation($"{x.PropertyName}.{x.ErrorCode}", x.ErrorMessage));
        var response = factoryMethodInfo.Invoke(null, [errors])!;

        return (TResponse)response;
    }
}
