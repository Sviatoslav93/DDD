using System.ComponentModel.DataAnnotations;

namespace ToDoList.Constants.Requests;

public class CompleteToDoItemRequest
{
    [Required]
    public required Guid ToDoListId { get; init; }

    [Required]
    public required Guid ToDoItemId { get; init; }
}
