using System.Diagnostics;

namespace BeggarMyNeighbourWeb.Services;

/// <summary>
/// Waits while responses to a broadcast request are collected, returning early once replies go
/// quiet rather than always burning the full window.
/// <para>
/// Messages are still handled the instant they arrive (the MQTT client is event-driven). This
/// only governs how long the <em>caller</em> keeps listening before giving up: it returns once no
/// new response has been seen for <c>idleGap</c>, or once <c>maxWindow</c> has elapsed, whichever
/// comes first.
/// </para>
/// </summary>
public sealed class ResponseWindow
{
    private readonly TimeSpan _maxWindow;
    private readonly TimeSpan _idleGap;
    private long _lastActivityTicks;

    public ResponseWindow(TimeSpan maxWindow, TimeSpan idleGap)
    {
        _maxWindow = maxWindow;
        _idleGap = idleGap;
    }

    /// <summary>Call from the message handler each time a relevant response is received.</summary>
    public void RecordActivity()
    {
        Interlocked.Exchange(ref _lastActivityTicks, Stopwatch.GetTimestamp());
    }

    /// <summary>
    /// Collects for up to the maximum window, returning early once the gap since the last recorded
    /// activity exceeds the idle gap. The idle timer only starts after the first response, so a
    /// slow first responder still gets the full window.
    /// </summary>
    public async Task WaitAsync(CancellationToken cancellationToken = default)
    {
        var start = Stopwatch.GetTimestamp();
        Interlocked.Exchange(ref _lastActivityTicks, 0);

        // Poll the activity timestamp in short slices. This is a cheap local loop, not a bus poll:
        // responses are still delivered to the handler the moment they arrive.
        var slice = TimeSpan.FromMilliseconds(25);

        while (true)
        {
            await Task.Delay(slice, cancellationToken);

            var now = Stopwatch.GetTimestamp();

            if (Stopwatch.GetElapsedTime(start, now) >= _maxWindow)
            {
                return;
            }

            var lastActivity = Interlocked.Read(ref _lastActivityTicks);

            // Only apply the idle cut-off once at least one response has arrived.
            if (lastActivity != 0 && Stopwatch.GetElapsedTime(lastActivity, now) >= _idleGap)
            {
                return;
            }
        }
    }
}
