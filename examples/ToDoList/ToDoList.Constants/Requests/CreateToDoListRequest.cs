using System.ComponentModel.DataAnnotations;
using ToDoList.Domain.Aggregates.ToDo.Restrictions;

namespace ToDoList.Constants.Requests;

public class CreateToDoListRequest
{
    [Required]
    [MaxLength(ToDoListRestrictions.TitleMaxLength)]
    public string Title { get; set; } = null!;
}
