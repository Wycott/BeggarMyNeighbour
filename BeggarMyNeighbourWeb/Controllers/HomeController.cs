using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BeggarMyNeighbourWeb.Models;
using BeggarMyNeighbourWeb.Services;

namespace BeggarMyNeighbourWeb.Controllers;

public class HomeController : Controller
{
    private readonly ISimulatorResultsProvider _resultsProvider;
    private readonly ISimulatorDiscoveryProvider _discoveryProvider;

    public HomeController(
        ISimulatorResultsProvider resultsProvider,
        ISimulatorDiscoveryProvider discoveryProvider)
    {
        _resultsProvider = resultsProvider;
        _discoveryProvider = discoveryProvider;
    }

    // The latest data is pulled from the simulators each time the dashboard is loaded or refreshed.
    public async Task<IActionResult> Index(bool latestOnly = false, CancellationToken cancellationToken = default)
    {
        var asOfUtc = DateTime.UtcNow;

        // Discover who's live on the bus (poll + wait window), and fetch results for the tabs.
        var discovered = await _discoveryProvider.DiscoverAsync(cancellationToken);
        var simulators = await _resultsProvider.GetSimulatorsAsync(cancellationToken);

        var model = new DashboardViewModel
        {
            LatestOnly = latestOnly,
            AsOfUtc = asOfUtc,
            ConnectedSimulatorCount = discovered.Count,
            Simulators = simulators
                .OrderBy(s => s.StartedUtc)
                .Select(s => new SimulatorViewModel(s, latestOnly, asOfUtc))
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
