namespace BeggarMyNeighbourWeb.Services;

/// <summary>
/// Display tuning for the dashboard. Bound from the "Dashboard" section of appsettings.json.
/// </summary>
public class DashboardSettings
{
    public const string SectionName = "Dashboard";

    /// <summary>
    /// How many times its normal output interval a simulator may be silent before it is flagged
    /// stale. For example, a value of 3 means a simulator that normally produces a result every
    /// 10 seconds is considered stale after 30 seconds of silence. Default 3.
    /// </summary>
    public double StaleMultiplier { get; set; } = 3.0;
}
