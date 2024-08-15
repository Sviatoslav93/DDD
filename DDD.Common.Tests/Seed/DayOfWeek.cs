using Domain.Common;

namespace DDD.Common.Tests.Seed;

public class DayOfWeek : Enumeration
{
    private DayOfWeek(int id, string name)
        : base(id, name)
    {
    }

    public static DayOfWeek Monday => new(1, "Monday");
    public static DayOfWeek Tuesday => new(2, "Tuesday");
    public static DayOfWeek Wednesday => new(3, "Wednesday");
    public static DayOfWeek Thursday => new(4, "Thursday");
    public static DayOfWeek Friday => new(5, "Friday");
    public static DayOfWeek Saturday => new(6, "Saturday");
    public static DayOfWeek Sunday => new(7, "Sunday");

    public bool IsWeekend => this == Saturday || this == Sunday;
    public bool IsWeekday => !IsWeekend;
}
