using Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToDoList.DataAccess.EntityConfigurations.Base;

public abstract class AuditableEntityConfiguration<TEntity, TId> : EntityConfiguration<TEntity, TId>
    where TEntity : AuditableEntity<TId>
    where TId : struct, IEquatable<TId>
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.CreatedBy)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired(false);

        builder.Property(x => x.UpdatedBy)
            .HasMaxLength(256)
            .IsRequired(false);
    }
}
