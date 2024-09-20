namespace ToDoList.Constants.Responses;

public readonly record struct ToDoItemResponse(
    Guid Id,
    string Title,
    string Description,
    DateTimeOffset CreatedDate,
    DateTimeOffset? DueDate,
    DateTimeOffset? CompletedDate,
    bool IsDone,
    bool IsFailed);
