namespace BeggarMyNeighbourLibrary.Helpers;

public static class RuntimeFormatter
{
    private const long MillisecondsPerSecond = 1000L;
    private const long MillisecondsPerMinute = 60L * MillisecondsPerSecond;
    private const long MillisecondsPerHour = 60L * MillisecondsPerMinute;
    private const long MillisecondsPerDay = 24L * MillisecondsPerHour;

    /// <summary>
    /// Formats an elapsed time in milliseconds using the largest unit that yields
    /// a whole-number value of at least 1 (days, hours, minutes, or seconds).
    /// </summary>
    public static string Format(long elapsedMilliseconds)
    {
        if (elapsedMilliseconds < 0)
        {
            elapsedMilliseconds = 0;
        }

        if (elapsedMilliseconds >= MillisecondsPerDay)
        {
            return Compose(elapsedMilliseconds / MillisecondsPerDay, "day");
        }

        if (elapsedMilliseconds >= MillisecondsPerHour)
        {
            return Compose(elapsedMilliseconds / MillisecondsPerHour, "hour");
        }

        if (elapsedMilliseconds >= MillisecondsPerMinute)
        {
            return Compose(elapsedMilliseconds / MillisecondsPerMinute, "min");
        }

        return Compose(elapsedMilliseconds / MillisecondsPerSecond, "sec");
    }

    private static string Compose(long value, string unit)
    {
        var plural = value == 1 ? string.Empty : "s";

        return $"Runtime {value:N0} {unit}{plural}";
    }
}
