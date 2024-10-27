using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Result;
using ToDoList.Application.ToDo.Persistence;
using ToDoList.Application.ToDo.Queries.Views;
using ToDoList.Errors;

namespace ToDoList.DataAccess.Queries;

public class ToDoListQueries(IConfiguration configuration, TimeProvider timeProvider) : IToDoListQueries
{
    private readonly string _connectionString = configuration.GetConnectionString("ToDoListDbConnection")
        ?? throw new ArgumentException("can not find connection string");

    public async Task<Result<ToDoListView>> GetToDoListById(Guid id, CancellationToken cancellationToken)
    {
        using var connection = CreateConnection();
        const string query = """
                             SELECT ToDoLists.Id,
                                    ToDoLists.Title,
                                    ToDoLists.CreatedBy,
                                    ToDoLists.CreatedAt,
                                    ToDoItems.Id,
                                    ToDoItems.Title,
                                    ToDoItems.Description,
                                    ToDoItems.CreatedDate,
                                    ToDoItems.DueDate,
                                    ToDoItems.CompletedDate,
                                    IIF(ToDoItems.DueDate < @Now AND ToDoItems.CompletedDate IS NULL, 1, 0) AS IsFailed,
                                    IIF(ToDoItems.CompletedDate IS NOT NULL, 1, 0) AS IsDone
                             FROM ToDoLists
                                      LEFT JOIN dbo.ToDoItems ON ToDoLists.Id = ToDoItems.ToDoListId
                                      WHERE ToDoLists.Id = @Id
                                      ORDER BY ToDoItems.DueDate
                             """;

        var toDoListDictionary = new Dictionary<Guid, ToDoListView>();

        await connection.QueryAsync<ToDoListView, ToDoItemView, ToDoListView>(
            query, (toDoList, toDoListItem) =>
            {
                var value = toDoListDictionary.GetValueOrDefault(toDoList.Id);
                if (value is null)
                {
                    value = toDoList;
                    toDoListDictionary.Add(value.Id, value);
                }

                // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
                if (toDoListItem != null && toDoListItem.Id != Guid.Empty)
                {
                    value.Items.Add(toDoListItem);
                }

                return value;
            },
#pragma warning disable SA1117
            new { Id = id, Now = timeProvider.GetUtcNow() },
#pragma warning restore SA1117
            splitOn: "Id");

        if (toDoListDictionary.TryGetValue(id, out var toDoListView))
        {
            return toDoListView;
        }

        return new NotFoundError("ToDoList not found");
    }

    public async Task<Result<ToDoListsView>> GetToDoLists(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        if (pageNumber < 1)
        {
            return new Error("Page number can not be less than 1");
        }

        var toDoListsCount = await GetToDoListsCount(cancellationToken);
        var pagesCount = (int)Math.Ceiling(toDoListsCount / (double)pageSize);

        if (pageNumber > pagesCount)
        {
            return new Error("Page number is greater than total pages count");
        }

        var from = (pageNumber - 1) * pageSize;
        var to = from + pageSize > toDoListsCount ? toDoListsCount : from + pageSize;

        using var connection = CreateConnection();
        const string query = """
                             SELECT *
                                 FROM ToDoLists
                                 ORDER BY Id
                                 OFFSET @From ROWS FETCH NEXT @To ROWS ONLY
                             """;

        var todoListsItems = await connection.QueryAsync<ToDoListsItemView>(query, new { From = from, To = to, Now = timeProvider.GetUtcNow() });

        return new ToDoListsView(todoListsItems, pageSize, pageNumber);
    }

    private async Task<int> GetToDoListsCount(CancellationToken cancellationToken)
    {
        using var connection = CreateConnection();
        const string query = "SELECT COUNT(*) FROM ToDoLists";

        return await connection.ExecuteScalarAsync<int>(query, cancellationToken);
    }

    private IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }
}
