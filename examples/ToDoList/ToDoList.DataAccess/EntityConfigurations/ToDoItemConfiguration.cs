using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.DataAccess.EntityConfigurations.Base;
using ToDoList.Domain.Aggregates.ToDo;
using ToDoList.Domain.Aggregates.ToDo.Restrictions;

namespace ToDoList.DataAccess.EntityConfigurations;

public class ToDoItemConfiguration : EntityConfiguration<ToDoItem, Guid>
{
    public override void Configure(EntityTypeBuilder<ToDoItem> builder)
    {
        base.Configure(builder);

        builder
            .Property(x => x.Title)
            .HasMaxLength(ToDoItemRestrictions.TitleMaxLength);

        builder
            .Property(x => x.Description)
            .HasMaxLength(ToDoItemRestrictions.DescriptionMaxLength);

        builder
            .HasIndex(x => new { x.Title, x.ToDoListId })
            .IsUnique();

        builder
            .Ignore(x => x.IsDone);
    }
}
