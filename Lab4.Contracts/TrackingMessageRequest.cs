namespace Lab4.Contracts;

public sealed class TrackingMessageRequest
{
    public string RoutingKey { get; init; } = string.Empty;

    public object Payload { get; init; } = new();
}