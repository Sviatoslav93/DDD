using MediatR;
using Result;

namespace ToDoList.Application.ToDo.Commands.ToDoListCreate;

public class ToDoListCreateCommand : IRequest<Result<Guid>>
{
    public string Title { get; set; } = null!;
}
