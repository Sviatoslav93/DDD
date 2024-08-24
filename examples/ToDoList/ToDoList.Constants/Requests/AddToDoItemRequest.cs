namespace ToDoList.Constants.Requests;

public class AddToDoItemRequest
{
    public Guid ToDoListId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTimeOffset DueDate { get; set; }
}
