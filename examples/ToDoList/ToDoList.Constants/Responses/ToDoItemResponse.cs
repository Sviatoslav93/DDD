namespace ToDoList.Constants.Responses;

public class ToDoItemResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset? DueDate { get; set; }
    public DateTimeOffset? CompletedDate { get; set; }
    public bool IsDone { get; set; }
    public bool IsFailed { get; set; }
}
