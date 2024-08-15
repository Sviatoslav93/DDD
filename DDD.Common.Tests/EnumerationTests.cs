using Domain.Common;
using FluentAssertions;
using DayOfWeek = DDD.Common.Tests.Seed.DayOfWeek;

namespace DDD.Common.Tests;

public class EnumerationTests
{
    [Fact]
    public void Should_GetAll_When_Requested()
    {
        var enumerations = Enumeration.GetAll<DayOfWeek>();

        enumerations.Should().NotBeEmpty().And.HaveCount(7);

        DayOfWeek[] test = [DayOfWeek.Saturday, DayOfWeek.Sunday];
        test.All(x => x.IsWeekend).Should().BeTrue();
    }
}
