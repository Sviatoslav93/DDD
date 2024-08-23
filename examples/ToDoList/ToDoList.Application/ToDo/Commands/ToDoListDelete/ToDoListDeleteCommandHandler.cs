using MediatR;
using Result;
using ToDoList.Application.ToDo.Persistence;

namespace ToDoList.Application.ToDo.Commands.ToDoListDelete;

public class ToDoListDeleteCommandHandler(IToDoListRepository repository) : IRequestHandler<ToDoListDeleteCommand, Result<Unit>>
{
    public Task<Result<Unit>> Handle(ToDoListDeleteCommand request, CancellationToken cancellationToken) =>
        repository.Delete(request.ToDoItemListId, cancellationToken);
}
