using Application.Common.Abstractions.Persistence;
using MediatR;
using Result;
using ToDoListEntity = ToDoList.Domain.Aggregates.ToDo.ToDoList;

namespace ToDoList.Application.ToDo.Persistence;

public interface IToDoListRepository : IRepository
{
    public Task<Result<ToDoListEntity>> GetById(Guid id, CancellationToken cancellationToken);

    Task<Result<Guid>> Add(ToDoListEntity toDoList, CancellationToken cancellationToken);

    Task<Result<Unit>> Delete(Guid id, CancellationToken cancellationToken);
}
