using MediatR;
using Result;

namespace ToDoList.Application.ToDo.Commands.ToDoItemUpdate;

public class ToDoItemUpdateCommand : IRequest<Result<Unit>>
{
    public Guid ToDoListId { get; set; }
    public Guid ToDoItemId { get; set; }

    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTimeOffset DueDate { get; set; }
}
