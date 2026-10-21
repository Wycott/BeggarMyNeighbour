using BeggarMyNeighbour.Messaging;
using BeggarMyNeighbourLibrary;
using BeggarMyNeighbourLibrary.Messaging;

namespace BeggarMyNeighbour;

internal partial class Program
{
    private static void Main()
    {
        // This simulator's identity, shared with the MQTT responder so it answers polls and
        // results requests with the same GUID it reports in its results.
        var simulator = new Simulator();

        // Start the MQTT responder in the background. It is best-effort: if the broker is
        // unavailable, log it and carry on simulating rather than failing to start.
        _ = StartMqttResponderAsync(simulator);

        RandomMode(simulator);
    }

    private static async Task StartMqttResponderAsync(Simulator simulator)
    {
        var settings = ReadMqttSettings();

        try
        {
            var responder = new SimulatorMqttResponder(simulator, settings);
            await responder.StartAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[mqtt] Responder not started ({settings.Host}:{settings.Port}): {ex.Message}");
        }
    }

    /// <summary>
    /// Reads broker settings from the Mqtt__Host / Mqtt__Port environment variables set by
    /// docker-compose, falling back to the <see cref="MqttSettings"/> defaults (localhost:1883).
    /// </summary>
    private static MqttSettings ReadMqttSettings()
    {
        var settings = new MqttSettings();

        var host = Environment.GetEnvironmentVariable("Mqtt__Host");

        if (!string.IsNullOrWhiteSpace(host))
        {
            settings.Host = host;
        }

        var portText = Environment.GetEnvironmentVariable("Mqtt__Port");

        if (int.TryParse(portText, out var port))
        {
            settings.Port = port;
        }

        return settings;
    }
}
