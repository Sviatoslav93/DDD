using System.Collections.Immutable;
using Microsoft.AspNetCore.Mvc;
using Result.Errors;

namespace ToDoList.Api.Helpers;

public static class ResultExtensions
{
    public static ProblemDetails ToProblemDetails(this IEnumerable<Error> errors)
    {
        var errorsList = errors.ToImmutableArray();
        var firstError = errorsList.First();

        var statusCode = firstError.ErrorType switch
        {
            ErrorType.Failure => StatusCodes.Status400BadRequest,
            ErrorType.Unexpected => StatusCodes.Status400BadRequest,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            _ => throw new ArgumentOutOfRangeException(),
        };

        return new ProblemDetails
        {
            Status = statusCode,
            Title = $"{firstError.ErrorType} error occured",
            Extensions = errorsList.ToDictionary(x => x.Code, error => (object)new { error.Description, error.ErrorType })!,
        };
    }
}
