namespace BeggarMyNeighbourLibrary.Messaging;

/// <summary>
/// Connection settings for the MQTT broker (Mosquitto). Bound from configuration in both apps:
/// the web app reads an "Mqtt" section of appsettings.json; the simulator reads the matching
/// Mqtt__Host / Mqtt__Port environment variables (set by docker-compose).
/// </summary>
public class MqttSettings
{
    /// <summary>The configuration section name both apps bind from.</summary>
    public const string SectionName = "Mqtt";

    /// <summary>Broker hostname. "localhost" for local dev; the service/NAS name in containers.</summary>
    public string Host { get; set; } = "localhost";

    /// <summary>Broker port. 1883 is the MQTT default for unencrypted traffic.</summary>
    public int Port { get; set; } = 1883;
}
