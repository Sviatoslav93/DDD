using System.ComponentModel.DataAnnotations;
using ToDoList.Domain.Aggregates.ToDo.Restrictions;

namespace ToDoList.Constants.Requests;

public class UpdateToDoListRequest
{
    public Guid ToDoListId { get; set; }

    [Required]
    [MaxLength(ToDoListRestrictions.TitleMaxLength)]
    public string Title { get; set; } = null!;
}
