using MediatR;
using Result;
using Result.Extensions;
using ToDoList.Application.ToDo.Persistence;

namespace ToDoList.Application.ToDo.Commands.ToDoItemUpdate;

public class ToDoItemUpdateCommandHandler(IToDoListRepository repository, TimeProvider timeProvider) : IRequestHandler<ToDoItemUpdateCommand, Result<Unit>>
{
    public Task<Result<Unit>> Handle(ToDoItemUpdateCommand request, CancellationToken cancellationToken) =>
        repository.GetById(request.ToDoListId, cancellationToken)
            .ThenAsync(x => x.UpdateItem(
                request.ToDoItemId,
                request.Title,
                request.Description,
                request.DueDate,
                timeProvider));
}
