namespace Lab4.Contracts;

public sealed class PatientEventRequest
{
    public string PatientId { get; init; } = string.Empty;

    public string EventType { get; init; } = string.Empty;

    public object Payload { get; init; } = new();
}