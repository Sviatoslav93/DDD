using System.ComponentModel.DataAnnotations;
using ToDoList.Domain.Aggregates.ToDo.Restrictions;

namespace ToDoList.Constants.Requests;

public class UpdateToDoItemRequest
{
    [Required]
    public Guid ToDoListId { get; set; }

    public Guid ToDoItemId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTimeOffset DueDate { get; set; }
}
