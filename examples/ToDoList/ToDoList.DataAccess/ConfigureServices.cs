using Application.Common.Abstractions.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Application.ToDo.Persistence;
using ToDoList.DataAccess.Contexts;
using ToDoList.DataAccess.Interseptors;
using ToDoList.DataAccess.Queries;
using ToDoList.DataAccess.Repositories;

namespace ToDoList.DataAccess;

public static class ConfigureServices
{
    public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();
        services.AddScoped<IToDoListRepository, ToDoListRepository>();
        services.AddScoped<IToDoListQueries, ToDoListQueries>();

        services.AddDbContext<ToDoListDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("ToDoListDbConnection"),
                builder => builder.MigrationsAssembly(typeof(ToDoListDbContext).Assembly.FullName)));

        services.AddScoped<ITransaction, ToDoListTransaction>();
        services.AddScoped<ToDoDbContextInitializer>();
        return services;
    }
}
