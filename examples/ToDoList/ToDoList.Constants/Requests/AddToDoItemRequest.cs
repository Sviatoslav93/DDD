namespace ToDoList.Constants.Requests;

public readonly record struct AddToDoItemRequest(
    Guid ToDoListId,
    string Title,
    string Description,
    DateTimeOffset DueDate);
