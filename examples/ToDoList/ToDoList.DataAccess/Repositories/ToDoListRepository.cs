using Application.Common.Abstractions.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Result;
using Result.Errors;
using ToDoList.Application.ToDo.Persistence;
using ToDoList.DataAccess.Contexts;
using ToDoListEntity = ToDoList.Domain.Aggregates.ToDo.ToDoList;

namespace ToDoList.DataAccess.Repositories;

public class ToDoListRepository(ToDoListDbContext context) : IToDoListRepository
{
    public IUnitOfWork UnitOfWork => context;

    public async Task<Result<ToDoListEntity>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var toDoList = await context.ToDoLists
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (toDoList is null)
        {
            return Error.NotFound("ToDoList.NotFound", $"entity with id {id} was not found");
        }

        return toDoList;
    }

    public async Task<Result<Guid>> Add(ToDoListEntity toDoList, CancellationToken cancellationToken)
    {
        var entry = await context.ToDoLists.AddAsync(toDoList, cancellationToken);

        return entry.Entity.Id;
    }

    public async Task<Result<Unit>> Delete(Guid id, CancellationToken cancellationToken)
    {
        var toDoList = await context.ToDoLists.FindAsync([id], cancellationToken: cancellationToken);

        if (toDoList is null)
        {
            return Error.NotFound("ToDoList.NotFound", $"entity with id {id} was not found");
        }

        context.ToDoLists.Remove(toDoList);
        return Unit.Value;
    }
}
