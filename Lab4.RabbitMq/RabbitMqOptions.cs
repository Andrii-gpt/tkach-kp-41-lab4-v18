namespace Lab4.RabbitMq;

public sealed class RabbitMqOptions
{
    public string HostName { get; init; } = "localhost";

    public int Port { get; init; } = 5672;

    public string UserName { get; init; } = "guest";

    public string Password { get; init; } = "guest";

    public string DirectExchangeName { get; init; } = "tracking.direct.exchange";

    public string TopicExchangeName { get; init; } = "patient.topic.exchange";
}