namespace BeggarMyNeighbourWeb.Models;

/// <summary>
/// Formats a <see cref="TimeSpan"/> as a compact, human-friendly duration for the dashboard,
/// showing the two most significant units (e.g. "3d 4h", "2h 5m", "47s").
/// </summary>
public static class DurationDisplay
{
    public static string Humanize(TimeSpan duration)
    {
        if (duration < TimeSpan.Zero)
        {
            duration = TimeSpan.Zero;
        }

        var days = (int)duration.TotalDays;
        var hours = duration.Hours;
        var minutes = duration.Minutes;
        var seconds = duration.Seconds;

        if (days > 0)
        {
            return Combine($"{days}d", hours > 0 ? $"{hours}h" : null);
        }

        if (hours > 0)
        {
            return Combine($"{hours}h", minutes > 0 ? $"{minutes}m" : null);
        }

        if (minutes > 0)
        {
            return Combine($"{minutes}m", seconds > 0 ? $"{seconds}s" : null);
        }

        return $"{seconds}s";
    }

    public static string Humanize(TimeSpan? duration)
    {
        return duration.HasValue ? Humanize(duration.Value) : "—";
    }

    private static string Combine(string primary, string? secondary)
    {
        return secondary is null ? primary : $"{primary} {secondary}";
    }
}
