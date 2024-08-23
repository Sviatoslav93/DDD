using MediatR;
using Result;

namespace ToDoList.Application.ToDo.Commands.ToDoListDelete;

public class ToDoListDeleteCommand : IRequest<Result<Unit>>
{
    public Guid ToDoItemListId { get; set; }
}
