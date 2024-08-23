using MediatR;
using Result;

namespace ToDoList.Application.ToDo.Commands.ToDoListUpdate;

public class ToDoListUpdateCommand : IRequest<Result<Unit>>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
}
