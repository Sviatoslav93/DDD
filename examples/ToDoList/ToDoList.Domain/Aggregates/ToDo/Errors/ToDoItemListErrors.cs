using Result.Errors;

namespace ToDoList.Domain.Aggregates.ToDo.Errors;

public static class ToDoItemListErrors
{
    public static Error ItemNotFound => Error.NotFound($"{nameof(ToDoList)}.ItemNotFound", "Item not found.");
    public static Error ItemWithSameTitleAlreadyExists => Error.Validation($"{nameof(ToDoList)}.ItemNotFound", "Item with the same title already exists.");
}
