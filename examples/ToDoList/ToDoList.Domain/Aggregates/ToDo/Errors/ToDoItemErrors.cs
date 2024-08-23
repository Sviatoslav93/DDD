using MediatR;
using Result;
using Result.Errors;

namespace ToDoList.Domain.Aggregates.ToDo.Errors;

public static class ToDoItemErrors
{
    public static Error FailedItemCannotBeUpdated => Error.Validation($"{nameof(ToDoItem)}.FailedItemCannotBeUpdated", "Failed item cannot be updated.");
    public static Error ItemAlreadyCompleted => Error.Validation($"{nameof(ToDoItem)}.ItemAlreadyCompleted", "Item already completed.");
    public static Error CompletedItemCannotBeUpdated => Error.Validation($"{nameof(ToDoItem)}.CompletedItemCannotBeUpdated", "Completed item cannot be updated.");
    public static Error DueDateCannotBeInPast => Error.Validation($"{nameof(ToDoItem)}.DueDateCannotBeInPast", "Due date cannot be in the past.");
    public static Error ItemDueDatePassed => Error.Validation($"{nameof(ToDoItem)}.ItemDueDatePassed", "Item should be already completed.");
}
