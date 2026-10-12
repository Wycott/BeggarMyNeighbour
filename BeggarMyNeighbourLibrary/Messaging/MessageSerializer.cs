using System.Buffers;
using System.Text.Json;

namespace BeggarMyNeighbourLibrary.Messaging;

/// <summary>
/// Serializes discovery messages to/from the UTF-8 JSON payloads carried on MQTT. Both the web
/// application and the simulators use this so the two sides always agree on the wire format.
/// </summary>
public static class MessageSerializer
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
    };

    /// <summary>Serializes a message to a UTF-8 JSON byte array suitable for an MQTT payload.</summary>
    public static byte[] Serialize<T>(T message)
    {
        return JsonSerializer.SerializeToUtf8Bytes(message, Options);
    }

    /// <summary>
    /// Deserializes a UTF-8 JSON MQTT payload back into a message, or returns null if the
    /// payload is empty or malformed. Returning null rather than throwing keeps a single bad
    /// message from taking down a subscriber's receive loop.
    /// </summary>
    public static T? Deserialize<T>(ReadOnlySpan<byte> payload) where T : class
    {
        if (payload.IsEmpty)
        {
            return null;
        }

        try
        {
            return JsonSerializer.Deserialize<T>(payload, Options);
        }
        catch (JsonException)
        {
            return null;
        }
    }

    /// <summary>
    /// Convenience overload for MQTTnet payloads, which arrive as a <see cref="ReadOnlySequence{T}"/>.
    /// </summary>
    public static T? Deserialize<T>(in ReadOnlySequence<byte> payload) where T : class
    {
        if (payload.IsEmpty)
        {
            return null;
        }

        try
        {
            var reader = new Utf8JsonReader(payload);

            return JsonSerializer.Deserialize<T>(ref reader, Options);
        }
        catch (JsonException)
        {
            return null;
        }
    }
}
