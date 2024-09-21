using System.ComponentModel.DataAnnotations;
using ToDoList.Domain.Aggregates.ToDo.Restrictions;

namespace ToDoList.Constants.Requests;

public class UpdateToDoListRequest
{
    [Required]
    public required Guid ToDoListId { get; init; }

    [Required]
    [MaxLength(ToDoListRestrictions.TitleMaxLength)]
    public required string Title { get; init; }
}
