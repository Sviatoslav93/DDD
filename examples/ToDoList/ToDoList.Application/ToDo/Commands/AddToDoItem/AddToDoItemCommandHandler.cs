using MediatR;
using Result;
using Result.Extensions;
using ToDoList.Application.ToDo.Persistence;
using ToDoList.Domain.Aggregates.ToDo;

namespace ToDoList.Application.ToDo.Commands.AddToDoItem;

public class AddToDoItemCommandHandler(
    TimeProvider timeProvider,
    IToDoListRepository repository)
    : IRequestHandler<AddToDoItemCommand, Result<Nothing>>
{
    public Task<Result<Nothing>> Handle(AddToDoItemCommand request, CancellationToken cancellationToken) =>
        repository.GetById(request.ToDoListId, cancellationToken)
            .ThenAsync(x => ToDoItem.Create(
                    request.Title,
                    request.Description,
                    request.DueDate,
                    timeProvider)
                .Match(
                    x.AddItem,
                    Result<Nothing>.Failed));
}
