using DDD.Common.Tests.Seed;
using FluentAssertions;

namespace DDD.Common.Tests;

public class ValueObjectTests
{
    [Fact]
    public void Should_EqualsReturnTrue_When_ValueObjectsHasTheSameProperties()
    {
        var valueObject1 = new TestValueObject("test");
        var valueObject2 = new TestValueObject("test");

        (valueObject1 == valueObject2).Should().BeTrue();
        valueObject1.Equals(valueObject2).Should().BeTrue();
    }

    [Fact]
    public void Should_EqualsReturnTrue_When_ValueObjectsHasTheDifferentValues()
    {
        var valueObject1 = new TestValueObject("test");
        var valueObject2 = new TestValueObject("test2");

        (valueObject1 == valueObject2).Should().BeFalse();
        valueObject1.Equals(valueObject2).Should().BeFalse();
    }
}
