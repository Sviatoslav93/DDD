using Result.Errors;

namespace ToDoList.Errors;

public static partial class ToDoListErrors
{
    public static Error ItemWithSameTitleAlreadyExists(string title)
        => Error.Validation(ErrorCodes.ItemWithSameTitleAlreadyExists, "Item with the same title already exists: " + title);
}
