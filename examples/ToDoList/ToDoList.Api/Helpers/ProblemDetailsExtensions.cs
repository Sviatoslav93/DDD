using System.Collections.Immutable;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Result;
using ToDoList.Errors;

namespace ToDoList.Api.Helpers;

public static class ProblemDetailsExtensions
{
    public static ProblemDetails ToProblemDetails(this IEnumerable<Error> errors)
    {
        var errorsList = errors.ToImmutableArray();
        var firstError = errorsList.First();

        if (errorsList.First() is ValidationError)
        {
            return MapValidationErrors(errorsList);
        }

        if (firstError is NotFoundError notFoundError)
        {
            return new ProblemDetails
            {
                Title = "Not found",
                Status = (int)HttpStatusCode.NotFound,
                Detail = notFoundError.Message,
            };
        }

        return new ProblemDetails
        {
            Title = "An error occured",
            Status = (int)HttpStatusCode.BadRequest,
            Detail = firstError.Message,
        };
    }

    private static ProblemDetails MapValidationErrors(ImmutableArray<Error> errorsList)
    {
        var problem = new ProblemDetails
        {
            Title = "Validation error occured",
            Status = (int)HttpStatusCode.BadRequest,
            Detail = "One or more validation errors occured",
        };

        problem.Extensions
            .Add("ValidationErrors", errorsList
                .Where(x => x is ValidationError)
                .Select(x => new { ((ValidationError)x).PropertyName, ((ValidationError)x).ErrorCode, ((ValidationError)x).Message }));

        return problem;
    }
}
