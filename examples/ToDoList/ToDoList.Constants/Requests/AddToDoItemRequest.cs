using System.ComponentModel.DataAnnotations;
using ToDoList.Domain.Aggregates.ToDo.Restrictions;

namespace ToDoList.Constants.Requests;

public class AddToDoItemRequest
{
    public Guid ToDoListId { get; set; }

    [Required]
    [MaxLength(ToDoItemRestrictions.TitleMaxLength)]
    public string Title { get; set; } = null!;

    [Required]
    [MaxLength(ToDoItemRestrictions.DescriptionMaxLength)]
    public string Description { get; set; } = null!;

    public DateTimeOffset DueDate { get; set; }
}
