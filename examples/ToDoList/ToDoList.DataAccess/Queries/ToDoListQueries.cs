using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Result;
using Result.Errors;
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

        return Error.NotFound("ToDoList not found", "ToDoList not found");
    }

    public async Task<Result<ToDoListsView>> GetToDoLists(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        if (pageNumber < 1)
        {
            return ToDoListErrors.InvalidPageNumber(pageNumber);
        }

        var toDoListsCount = await GetToDoListsCount(cancellationToken);
        var pagesCount = (int)Math.Ceiling(toDoListsCount / (double)pageSize);

        if (pageNumber > pagesCount)
        {
            return ToDoListErrors.InvalidPageNumber(pageNumber);
        }

        var from = (pageNumber - 1) * pageSize;
        var to = from + pageSize > toDoListsCount ? toDoListsCount : from + pageSize;

        using var connection = CreateConnection();
        const string query = """
                             WITH LimitedToDoLists AS (
                                 SELECT *
                                 FROM ToDoLists
                                 ORDER BY Id
                                 OFFSET @From ROWS FETCH NEXT @To ROWS ONLY
                             )
                             SELECT LimitedToDoLists.Id,
                                    LimitedToDoLists.Title,
                                    LimitedToDoLists.CreatedBy,
                                    LimitedToDoLists.CreatedAt,
                                    ToDoItems.Id AS ToDoItemId,
                                    ToDoItems.Id,
                                    ToDoItems.Title,
                                    ToDoItems.Description,
                                    ToDoItems.CreatedDate,
                                    ToDoItems.DueDate,
                                    ToDoItems.CompletedDate,
                                    IIF(ToDoItems.DueDate < @Now AND ToDoItems.CompletedDate IS NULL, 1, 0) AS IsFailed,
                                    IIF(ToDoItems.CompletedDate IS NOT NULL, 1, 0) AS IsDone
                             FROM LimitedToDoLists
                                      LEFT JOIN dbo.ToDoItems ON LimitedToDoLists.Id = ToDoItems.ToDoListId
                             ORDER BY LimitedToDoLists.Id, ToDoItems.Id;
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

                if (toDoListItem.Id != Guid.Empty)
                {
                    value.Items.Add(toDoListItem);
                }

                return value;
            },
#pragma warning disable SA1117
            new { From = from, To = to, Now = timeProvider.GetUtcNow() },
            splitOn: "ToDoItemId");
#pragma warning restore SA1117

        return new ToDoListsView(toDoListDictionary.Select(x => x.Value), pageSize, pageNumber);
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
