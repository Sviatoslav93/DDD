using Result.Errors;

namespace ToDoList.Errors;

public static partial class ToDoListErrors
{
    public static Error ItemAlreadyFailed(Guid id) => Error.Failure(
        ErrorCodes.ItemAlreadyUpdated,
        $"Item with id '{id}' already failed.");

    public static Error ItemAlreadyCompleted(Guid id) => Error.Failure(
        ErrorCodes.ItemAlreadyCompleted,
        $"Item with id '{id}' already completed.");

    public static Error DueDateCannotBeInPast(Guid id, DateTimeOffset dueDate) => Error.Failure(
        ErrorCodes.ItemDueDateCannotBeInPast,
        $"Due date '{dueDate}' for item with id '{id}' cannot be in the past.");

    public static Error ItemDueDatePassed(Guid id, DateTimeOffset dueDate) => Error.Failure(
        ErrorCodes.ItemDueDatePassed,
        $"Due date '{dueDate}' for item with id '{id}' has already passed.");
}
