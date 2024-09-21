namespace ToDoList.Constants.Responses;

public class ToDoListResponse
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string CreatedBy { get; init; }
    public required DateTimeOffset CreatedAt { get; init; }
    public string? UpdatedBy { get; init; }
    public DateTimeOffset? UpdatedAt { get; init; }
    public required IEnumerable<ToDoItemResponse> Items { get; init; } = [];
}
