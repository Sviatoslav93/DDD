using System.Reflection;
using DataAccess.Common.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoList.DataAccess.Contexts.Base;
using ToDoList.DataAccess.Interseptors;
using ToDoList.Domain.Aggregates.ToDo;
using ToDoListEntity = ToDoList.Domain.Aggregates.ToDo.ToDoList;

namespace ToDoList.DataAccess.Contexts;

public class ToDoListDbContext : BaseDbContext<ToDoListDbContext>
{
    public ToDoListDbContext(DbContextOptions<ToDoListDbContext> options)
        : base(options)
    {
    }

    public ToDoListDbContext(DbContextOptions<ToDoListDbContext> options, AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor, IMediator mediator)
        : base(options, auditableEntitySaveChangesInterceptor, mediator)
    {
    }

    public DbSet<ToDoListEntity> ToDoLists { get; set; } = null!;
    public DbSet<ToDoItem> ToDoItems { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
