using MediatR;
using Result;

namespace ToDoList.Application.ToDo.Commands.ToDoItemDelete;

public class ToDoItemDeleteCommand : IRequest<Result<Unit>>
{
    public Guid ToDoListId { get; set; }
    public Guid ToDoItemId { get; set; }
}
