using MediatR;
using Result;
using Result.Extensions;
using ToDoList.Application.ToDo.Persistence;
using ToDoList.Domain.Aggregates.ToDo;

namespace ToDoList.Application.ToDo.Commands.AddToDoItem;

public class AddToDoItemCommandHandler(
    TimeProvider timeProvider,
    IToDoListRepository repository)
    : IRequestHandler<AddToDoItemCommand, Result<Unit>>
{
    public Task<Result<Unit>> Handle(AddToDoItemCommand request, CancellationToken cancellationToken) =>
        repository.GetById(request.ToDoListId, cancellationToken)
            .ThenAsync(x =>
            {
                var (todoItem, failure) = ToDoItem.Create(
                    request.Title,
                    request.Description,
                    request.DueDate,
                    timeProvider);

                return failure ? failure : x.AddItem(todoItem!);
            });
}
