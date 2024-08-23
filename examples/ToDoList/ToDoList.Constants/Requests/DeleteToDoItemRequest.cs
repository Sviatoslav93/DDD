namespace ToDoList.Constants.Requests;

public class DeleteToDoItemRequest
{
    public Guid ToDoListId { get; set; }
    public Guid ToDoItemId { get; set; }
}
