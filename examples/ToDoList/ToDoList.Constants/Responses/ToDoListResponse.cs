namespace ToDoList.Constants.Responses;

public readonly record struct ToDoListResponse(
    Guid Id,
    string Title,
    string CreatedBy,
    string CreatedAt,
    string? UpdatedBy,
    string? UpdatedAt,
    IEnumerable<ToDoItemResponse> Items);
