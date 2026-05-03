namespace Lab4.Contracts;

public sealed class MessageEnvelope
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public DateTime TimestampUtc { get; init; } = DateTime.UtcNow;

    public string RoutingKey { get; init; } = string.Empty;

    public object Payload { get; init; } = new();
}