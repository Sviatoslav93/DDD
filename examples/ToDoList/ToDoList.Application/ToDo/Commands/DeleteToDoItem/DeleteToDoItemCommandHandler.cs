using MediatR;
using Result;
using Result.Extensions;
using ToDoList.Application.ToDo.Persistence;

namespace ToDoList.Application.ToDo.Commands.DeleteToDoItem;

public class DeleteToDoItemCommandHandler(IToDoListRepository repository) : IRequestHandler<DeleteToDoItemCommand, Result<Unit>>
{
    public Task<Result<Unit>> Handle(DeleteToDoItemCommand request, CancellationToken cancellationToken) =>
        repository.GetById(request.ToDoListId, cancellationToken)
            .ThenAsync(x => x.DeleteItem(request.ToDoItemId));
}
