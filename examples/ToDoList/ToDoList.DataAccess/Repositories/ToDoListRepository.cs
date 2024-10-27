using Application.Common.Abstractions.Persistence;
using Microsoft.EntityFrameworkCore;
using Result;
using ToDoList.Application.ToDo.Persistence;
using ToDoList.DataAccess.Contexts;
using ToDoList.Errors;
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
            return new NotFoundError("ToDo list not found");
        }

        return toDoList;
    }

    public async Task<Result<Guid>> Add(ToDoListEntity toDoList, CancellationToken cancellationToken)
    {
        var entry = await context.ToDoLists.AddAsync(toDoList, cancellationToken);

        return entry.Entity.Id;
    }

    public async Task<Result<Nothing>> Delete(Guid id, CancellationToken cancellationToken)
    {
        var toDoList = await context.ToDoLists.FindAsync([id], cancellationToken: cancellationToken);

        if (toDoList is null)
        {
            return new NotFoundError("ToDo list not found");
        }

        context.ToDoLists.Remove(toDoList);
        return Nothing.Value;
    }
}
