using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToDoList.DataAccess.EntityConfigurations.Base;

public abstract class EntityConfiguration<TEntity, TId> : IEntityTypeConfiguration<TEntity>
    where TEntity : Entity<TId>
    where TId : struct, IEquatable<TId>
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Ignore(x => x.DomainEvents);
        builder.HasKey(x => x.Id);
    }
}
