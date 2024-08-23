using MediatR;
using Result;

namespace ToDoList.Application.ToDo.Commands.ToDoItemComplete;

public class ToDoItemCompleteCommand(Guid toDoListId, Guid toDoItemId) : IRequest<Result<Unit>>
{
    public Guid ToDoListId { get; } = toDoListId;
    public Guid ToDoItemId { get; } = toDoItemId;
}
