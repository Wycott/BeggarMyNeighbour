using System.Buffers;
using System.Text;
using AiAnnotations;
using BeggarMyNeighbourLibrary.Messaging;

namespace BeggarMyNeighbourLibrary.Test;

[AiGenerated]
public class MessageSerializerTest
{
    [Fact]
    public void DiscoveryPoll_RoundTrips()
    {
        var original = new DiscoveryPoll
        {
            PollId = Guid.NewGuid(),
            IssuedUtc = new DateTime(2026, 10, 12, 9, 30, 0, DateTimeKind.Utc),
        };

        var bytes = MessageSerializer.Serialize(original);
        var restored = MessageSerializer.Deserialize<DiscoveryPoll>(bytes);

        Assert.NotNull(restored);
        Assert.Equal(original.PollId, restored!.PollId);
        Assert.Equal(original.IssuedUtc, restored.IssuedUtc);
    }

    [Fact]
    public void DiscoveryResponse_RoundTrips()
    {
        var original = new DiscoveryResponse
        {
            PollId = Guid.NewGuid(),
            SimulatorId = Guid.NewGuid(),
            StartedUtc = new DateTime(2026, 10, 11, 22, 15, 0, DateTimeKind.Utc),
        };

        var bytes = MessageSerializer.Serialize(original);
        var restored = MessageSerializer.Deserialize<DiscoveryResponse>(bytes);

        Assert.NotNull(restored);
        Assert.Equal(original.PollId, restored!.PollId);
        Assert.Equal(original.SimulatorId, restored.SimulatorId);
        Assert.Equal(original.StartedUtc, restored.StartedUtc);
    }

    [Fact]
    public void Serialize_UsesCamelCasePropertyNames()
    {
        var poll = new DiscoveryPoll { PollId = Guid.NewGuid(), IssuedUtc = DateTime.UtcNow };

        var json = Encoding.UTF8.GetString(MessageSerializer.Serialize(poll));

        Assert.Contains("\"pollId\"", json);
        Assert.Contains("\"issuedUtc\"", json);
        Assert.DoesNotContain("\"PollId\"", json);
    }

    [Fact]
    public void Deserialize_IsCaseInsensitive()
    {
        // A payload using PascalCase property names should still deserialize.
        var pollId = Guid.NewGuid();
        var json = $"{{\"PollId\":\"{pollId}\",\"IssuedUtc\":\"2026-10-12T09:30:00Z\"}}";
        var bytes = Encoding.UTF8.GetBytes(json);

        var restored = MessageSerializer.Deserialize<DiscoveryPoll>(bytes);

        Assert.NotNull(restored);
        Assert.Equal(pollId, restored!.PollId);
    }

    [Fact]
    public void Deserialize_EmptyPayload_ReturnsNull()
    {
        var restored = MessageSerializer.Deserialize<DiscoveryPoll>(ReadOnlySpan<byte>.Empty);

        Assert.Null(restored);
    }

    [Fact]
    public void Deserialize_MalformedPayload_ReturnsNull()
    {
        var bytes = Encoding.UTF8.GetBytes("{ this is not json ]");

        var restored = MessageSerializer.Deserialize<DiscoveryPoll>(bytes);

        Assert.Null(restored);
    }

    [Fact]
    public void Deserialize_ReadOnlySequenceOverload_RoundTrips()
    {
        var original = new DiscoveryResponse
        {
            PollId = Guid.NewGuid(),
            SimulatorId = Guid.NewGuid(),
            StartedUtc = new DateTime(2026, 10, 10, 8, 0, 0, DateTimeKind.Utc),
        };

        var sequence = new ReadOnlySequence<byte>(MessageSerializer.Serialize(original));
        var restored = MessageSerializer.Deserialize<DiscoveryResponse>(sequence);

        Assert.NotNull(restored);
        Assert.Equal(original.SimulatorId, restored!.SimulatorId);
        Assert.Equal(original.PollId, restored.PollId);
    }

    [Fact]
    public void Deserialize_ReadOnlySequenceOverload_EmptyReturnsNull()
    {
        var restored = MessageSerializer.Deserialize<DiscoveryResponse>(ReadOnlySequence<byte>.Empty);

        Assert.Null(restored);
    }
}
