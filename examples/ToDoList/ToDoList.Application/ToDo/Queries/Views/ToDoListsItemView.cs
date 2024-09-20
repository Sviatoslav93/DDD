namespace ToDoList.Application.ToDo.Queries.Views;

public class ToDoListsItemView
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string CreatedBy { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
