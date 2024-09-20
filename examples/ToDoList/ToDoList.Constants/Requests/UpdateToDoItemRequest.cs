namespace ToDoList.Constants.Requests;

public readonly record struct UpdateToDoItemRequest(
    Guid ToDoListId,
    Guid ToDoItemId,
    string Title,
    string Description,
    DateTimeOffset DueDate);
