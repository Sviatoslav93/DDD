namespace ToDoList.Constants.Responses;

public class ToDoListsItemResponse
{
    public Guid Id { get; init; }
    public required string Title { get; init; }
    public required string CreatedBy { get; init; }
    public required DateTimeOffset CreatedAt { get; init; }
    public string? UpdatedBy { get; init; }
    public DateTimeOffset? UpdatedAt { get; init; }
}
