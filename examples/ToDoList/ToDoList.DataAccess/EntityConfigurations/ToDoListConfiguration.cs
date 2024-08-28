using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.DataAccess.EntityConfigurations.Base;
using ToDoList.Domain.Aggregates.ToDo.Restrictions;

namespace ToDoList.DataAccess.EntityConfigurations;

public class ToDoListConfiguration : AuditableEntityConfiguration<Domain.Aggregates.ToDo.ToDoList, Guid>
{
    public override void Configure(EntityTypeBuilder<Domain.Aggregates.ToDo.ToDoList> builder)
    {
        base.Configure(builder);

        builder
            .Property(x => x.Title)
            .HasMaxLength(ToDoListRestrictions.TitleMaxLength);

        builder
            .HasMany(x => x.Items)
            .WithOne()
            .HasForeignKey(x => x.ToDoListId)
            .IsRequired();
    }
}
