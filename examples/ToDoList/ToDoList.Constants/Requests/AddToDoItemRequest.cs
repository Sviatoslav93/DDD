using System.ComponentModel.DataAnnotations;
using ToDoList.Domain.Aggregates.ToDo.Restrictions;

namespace ToDoList.Constants.Requests;

public class AddToDoItemRequest
{
    [Required]
    public required Guid ToDoListId { get; init; }

    [Required]
    [MaxLength(ToDoItemRestrictions.TitleMaxLength)]
    [MinLength(1)]
    public required string Title { get; init; }

    [Required]
    [MaxLength(ToDoItemRestrictions.DescriptionMaxLength)]
    [MinLength(1)]
    public required string Description { get; init; }

    public required DateTimeOffset DueDate { get; init; }
}
