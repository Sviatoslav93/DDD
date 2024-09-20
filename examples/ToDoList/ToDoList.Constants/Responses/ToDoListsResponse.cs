namespace ToDoList.Constants.Responses;

public class ToDoListsResponse
{
    public int ItemsCount { get; set; }
    public int PagesCount { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public IEnumerable<ToDoListsItemResponse> Items { get; set; } = [];
}
