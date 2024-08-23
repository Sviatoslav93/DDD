using Result;
using ToDoList.Application.ToDo.Queries.Views;

namespace ToDoList.Application.ToDo.Persistence;

public interface IToDoListQueries
{
    Task<Result<ToDoListView>> GetToDoListById(Guid id, CancellationToken cancellationToken);
    Task<Result<ToDoListsView>> GetToDoLists(int pageNumber, int pageSize, CancellationToken cancellationToken);
}
