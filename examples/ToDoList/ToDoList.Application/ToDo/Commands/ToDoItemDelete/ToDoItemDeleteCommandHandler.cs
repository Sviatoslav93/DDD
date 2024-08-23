using MediatR;
using Result;
using Result.Extensions;
using ToDoList.Application.ToDo.Persistence;

namespace ToDoList.Application.ToDo.Commands.ToDoItemDelete;

public class ToDoItemDeleteCommandHandler(IToDoListRepository repository) : IRequestHandler<ToDoItemDeleteCommand, Result<Unit>>
{
    public Task<Result<Unit>> Handle(ToDoItemDeleteCommand request, CancellationToken cancellationToken) =>
        repository.GetById(request.ToDoListId, cancellationToken)
            .ThenAsync(x => x.DeleteItem(request.ToDoItemId));
}
