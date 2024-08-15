using Domain.Common;

namespace DDD.Common.Tests.Seed;

public class TestValueObject(
    string test)
    : ValueObject
{
    public string Test { get; set; } = test;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Test;
    }
}
