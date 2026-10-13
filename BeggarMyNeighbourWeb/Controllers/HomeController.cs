using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using BeggarMyNeighbourWeb.Models;
using BeggarMyNeighbourWeb.Services;

namespace BeggarMyNeighbourWeb.Controllers;

public class HomeController : Controller
{
    private readonly ISimulatorResultsProvider _resultsProvider;
    private readonly ISimulatorDiscoveryProvider _discoveryProvider;
    private readonly DashboardSettings _dashboardSettings;

    public HomeController(
        ISimulatorResultsProvider resultsProvider,
        ISimulatorDiscoveryProvider discoveryProvider,
        IOptions<DashboardSettings> dashboardSettings)
    {
        _resultsProvider = resultsProvider;
        _discoveryProvider = discoveryProvider;
        _dashboardSettings = dashboardSettings.Value;
    }

    // The latest data is pulled from the simulators each time the dashboard is loaded or refreshed.
    // The "latest only" toggle is handled client-side and does not reload the page.
    public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
    {
        var asOfUtc = DateTime.UtcNow;
        var stopwatch = Stopwatch.StartNew();

        // Discover who's live on the bus (poll + wait window), and fetch results for the tabs.
        var discovered = await _discoveryProvider.DiscoverAsync(cancellationToken);
        var simulators = await _resultsProvider.GetSimulatorsAsync(cancellationToken);

        stopwatch.Stop();

        var model = new DashboardViewModel
        {
            AsOfUtc = asOfUtc,
            FetchDuration = stopwatch.Elapsed,
            ConnectedSimulatorCount = discovered.Count,
            Simulators = simulators
                .OrderBy(s => s.StartedUtc)
                .Select(s => new SimulatorViewModel(s, asOfUtc, _dashboardSettings.StaleMultiplier))
                .ToList(),
        };

        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
