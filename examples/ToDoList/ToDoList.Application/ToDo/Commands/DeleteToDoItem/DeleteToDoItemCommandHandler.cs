using MediatR;
using Result;
using Result.Extensions;
using ToDoList.Application.ToDo.Persistence;

namespace ToDoList.Application.ToDo.Commands.DeleteToDoItem;

public class DeleteToDoItemCommandHandler(IToDoListRepository repository) : IRequestHandler<DeleteToDoItemCommand, Result<Nothing>>
{
    public Task<Result<Nothing>> Handle(DeleteToDoItemCommand request, CancellationToken cancellationToken) =>
        repository.GetById(request.ToDoListId, cancellationToken)
            .ThenAsync(x => x.DeleteItem(request.ToDoItemId));
}
