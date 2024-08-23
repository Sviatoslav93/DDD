using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Result;
using ToDoList.Application.ToDo.Persistence;
using ToDoList.Application.ToDo.Queries.Views;

namespace ToDoList.DataAccess.Queries;

public class ToDoListQueries(IConfiguration configuration) : IToDoListQueries
{
    private readonly string _connectionString = configuration.GetConnectionString("DbConnection")
        ?? throw new ArgumentException("can not find connection string");

    public Task<Result<ToDoListView>> GetToDoListById(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result<ToDoListsView>> GetToDoLists(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }
}
