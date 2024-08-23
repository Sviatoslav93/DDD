namespace ToDoList.Application.ToDo.Queries.Views;

public class ToDoListsView
{
    public ToDoListsView(IEnumerable<ToDoListView> items, int pageSize, int pageNumber)
    {
        Items = items;
        ItemsCount = Items.Count();
        PageSize = pageSize;
        PageNumber = pageNumber;
        PagesCount = (int)Math.Ceiling((double)ItemsCount / PageSize);
    }

    public int ItemsCount { get; }
    public int PagesCount { get; }
    public int PageSize { get; }
    public int PageNumber { get; }
    public IEnumerable<ToDoListView> Items { get; }
}
