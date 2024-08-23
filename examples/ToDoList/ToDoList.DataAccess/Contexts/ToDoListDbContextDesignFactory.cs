using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ToDoList.DataAccess.Contexts;

public class ToDoListDbContextDesignFactory : IDesignTimeDbContextFactory<ToDoListDbContext>
{
    public ToDoListDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ToDoListDbContext>()
            .UseSqlServer("Server=.;Database=ToDoList;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False;");

        return new ToDoListDbContext(optionsBuilder.Options);
    }
}
