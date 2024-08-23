using ToDoList.DataAccess.Contexts.Base;

namespace ToDoList.DataAccess.Contexts;

public class ToDoListTransaction(ToDoListDbContext context) : Transaction<ToDoListDbContext>(context)
{
}
