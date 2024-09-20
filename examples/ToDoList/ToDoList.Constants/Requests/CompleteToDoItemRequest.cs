namespace ToDoList.Constants.Requests;

public readonly record struct CompleteToDoItemRequest(
    Guid ToDoListId,
    Guid ToDoItemId);
