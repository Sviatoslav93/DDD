﻿using MediatR;
using Result;
using Result.Extensions;
using ToDoList.Application.ToDo.Persistence;

namespace ToDoList.Application.ToDo.Commands.CompleteToDoItem;

public class CompleteToDoItemCommandHandler(IToDoListRepository repository, TimeProvider timeProvider) : IRequestHandler<CompleteToDoItemCommand, Result<Nothing>>
{
    public Task<Result<Nothing>> Handle(CompleteToDoItemCommand request, CancellationToken cancellationToken) =>
        repository.GetById(request.ToDoListId, cancellationToken)
            .ThenAsync(x => x.CompleteItem(request.ToDoItemId, timeProvider));
}
