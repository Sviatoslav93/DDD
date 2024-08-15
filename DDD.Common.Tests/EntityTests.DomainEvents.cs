using DDD.Common.Tests.Seed;
using FluentAssertions;

namespace DDD.Common.Tests;

public partial class EntityTests
{
    [Fact]
    public void Should_AddDomainEvent_When_Added()
    {
        var entity = new TestEntity
        {
            IsActive = true,
        };

        entity.DomainEvents.Should().NotBeEmpty();
    }

    [Fact]
    public void Should_ClearDomain_When_Added()
    {
        var entity = new TestEntity
        {
            IsActive = true,
        };

        entity.ClearDomainEvents();

        entity.DomainEvents.Should().BeEmpty();
    }
}
