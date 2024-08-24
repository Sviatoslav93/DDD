namespace ToDoList.Constants.Requests;

public class CompleteToDoItemRequest
{
    public Guid ToDoListId { get; set; }

    public Guid ToDoItemId { get; set; }
}
