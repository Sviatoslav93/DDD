using Result.Errors;

namespace ToDoList.Errors;

public static partial class ToDoListErrors
{
    public static Error NotFound<T>(T id) => Error.NotFound(ErrorCodes.NotFound, $"Item with id '{id}' was not found");
    public static Error UrlMismatch(string url) => Error.Failure(ErrorCodes.UrlMismatch, $"Url mismatch: {url}");

    public static Error InvalidPageNumber(int pageNumber) => Error.Failure(ErrorCodes.InvalidPageNumber, $"Invalid page number: {pageNumber}");

    public static Error InvalidPageSize(int pageSize) => Error.Failure(ErrorCodes.InvalidPageSize, $"Invalid page size: {pageSize}");
}
