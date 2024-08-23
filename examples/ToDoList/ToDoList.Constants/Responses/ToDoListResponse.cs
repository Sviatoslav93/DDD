namespace ToDoList.Constants.Responses;

public class ToDoListResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;
    public string CreatedAt { get; set; } = null!;

    public string? UpdatedBy { get; set; }
    public string? UpdatedAt { get; set; }

    public IEnumerable<ToDoItemResponse> Items { get; set; } = [];
}
