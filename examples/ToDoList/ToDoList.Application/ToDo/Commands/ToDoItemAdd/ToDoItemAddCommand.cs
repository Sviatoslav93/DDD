using MediatR;
using Result;

namespace ToDoList.Application.ToDo.Commands.ToDoItemAdd;

public class ToDoItemAddCommand : IRequest<Result<Unit>>
{
    public Guid ToDoListId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTimeOffset DueDate { get; set; }
}
