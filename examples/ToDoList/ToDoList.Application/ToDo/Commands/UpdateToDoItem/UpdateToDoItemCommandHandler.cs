using MediatR;
using Result;
using Result.Extensions;
using ToDoList.Application.ToDo.Persistence;

namespace ToDoList.Application.ToDo.Commands.UpdateToDoItem;

public class UpdateToDoItemCommandHandler(IToDoListRepository repository, TimeProvider timeProvider) : IRequestHandler<UpdateToDoItemCommand, Result<Nothing>>
{
    public Task<Result<Nothing>> Handle(UpdateToDoItemCommand request, CancellationToken cancellationToken) =>
        repository.GetById(request.ToDoListId, cancellationToken)
            .ThenAsync(x => x.UpdateItem(
                request.ToDoItemId,
                request.Title,
                request.Description,
                request.DueDate,
                timeProvider));
}
