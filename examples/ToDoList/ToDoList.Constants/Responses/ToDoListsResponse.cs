namespace ToDoList.Constants.Responses;

public class ToDoListsResponse
{
    public int ItemsCount { get; init; }
    public int PagesCount { get; init; }
    public int PageSize { get; init; }
    public int PageNumber { get; init; }
    public IEnumerable<ToDoListsItemResponse> Items { get; set; } = [];
}
