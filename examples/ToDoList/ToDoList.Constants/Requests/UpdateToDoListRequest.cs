namespace ToDoList.Constants.Requests;

public readonly record struct UpdateToDoListRequest(
    Guid ToDoListId,
    string Title);
