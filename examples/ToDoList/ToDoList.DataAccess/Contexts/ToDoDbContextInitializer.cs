using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace ToDoList.DataAccess.Contexts;

public class ToDoDbContextInitializer(
    ILogger<ToDoDbContextInitializer> logger,
    ToDoListDbContext context)
{
    public async Task InitialiseAsync()
    {
        try
        {
            if (context.Database.IsSqlServer())
            {
                await context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initialising the database");
            throw;
        }
    }
}
