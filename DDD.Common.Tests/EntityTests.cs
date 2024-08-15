using DDD.Common.Tests.Seed;
using FluentAssertions;

namespace DDD.Common.Tests;

public partial class EntityTests
{
    [Fact]
    public void Should_BeTransient_When_IdIsDefault()
    {
        var entity = new TestEntity();

        entity.IsTransient.Should().BeTrue();
    }

    [Fact]
    public void Should_NotBeEqual_When_Transient()
    {
        var entity1 = new TestEntity();
        var entity2 = new TestEntity();

        (entity1 != entity2).Should().BeTrue();
        entity1.Equals(entity2).Should().BeFalse();
    }

    [Fact]
    public void Should_HaveDefaultHashCode_When_Transient()
    {
        var entity1 = new TestEntity();
        var entity2 = new TestEntity();

        (entity1.GetHashCode() != entity2.GetHashCode()).Should().BeTrue();
    }

    [Fact]
    public void Should_BeEqual_When_NotTransientWithTheSameId()
    {
        var id = Guid.NewGuid();
        var entity1 = new TestEntity
        {
            Id = id,
        };
        var entity2 = new TestEntity
        {
            Id = id,
        };

        (entity1 == entity2).Should().BeTrue();
        entity1.Equals(entity2).Should().BeTrue();
    }

    [Fact]
    public void Should_HaveSameHashCode_When_NotTransientWithTheSameId()
    {
        var id = Guid.NewGuid();
        var entity1 = new TestEntity
        {
            Id = id,
        };
        var entity2 = new TestEntity
        {
            Id = id,
        };

        (entity1.GetHashCode() == entity2.GetHashCode()).Should().BeTrue();
    }

    [Fact]
    public void Should_NotBeEqual_When_NotTransientWithDifferentId()
    {
        var entity1 = new TestEntity
        {
            Id = Guid.NewGuid(),
        };
        var entity2 = new TestEntity
        {
            Id = Guid.NewGuid(),
        };

        (entity1 != entity2).Should().BeTrue();
    }

    [Fact]
    public void Should_HaveDifferentHashCode_When_NotTransientWithDifferentId()
    {
        var entity1 = new TestEntity
        {
            Id = Guid.NewGuid(),
        };
        var entity2 = new TestEntity
        {
            Id = Guid.NewGuid(),
        };

        (entity1.GetHashCode() != entity2.GetHashCode()).Should().BeTrue();
    }

    [Fact]
    public void Should_ThrowArgumentException_When_SetDefaultId()
    {
        var entity1 = new TestEntity();

        FluentActions.Invoking(() => entity1.Id = default)
            .Should()
            .Throw<ArgumentException>()
            .WithMessage("The ID cannot be the default value. (Parameter 'Id')");
    }
}
