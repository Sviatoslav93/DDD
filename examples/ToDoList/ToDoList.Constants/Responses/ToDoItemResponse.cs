namespace ToDoList.Constants.Responses;

public class ToDoItemResponse
{
    public Guid Id { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public DateTimeOffset CreatedDate { get; init; }
    public DateTimeOffset? DueDate { get; init; }
    public DateTimeOffset? CompletedDate { get; init; }
    public bool IsDone { get; init; }
    public bool IsFailed { get; init; }
}
