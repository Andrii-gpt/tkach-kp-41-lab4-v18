using System.Text;
using System.Text.Json;
using Lab4.Contracts;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Lab4.RabbitMq;

public sealed class RabbitMqPublisher : IRabbitMqPublisher
{
    private readonly RabbitMqOptions _options;

    public RabbitMqPublisher(IOptions<RabbitMqOptions> options)
    {
        _options = options.Value;
    }

    public async Task PublishToDirectExchangeAsync(string routingKey, object payload)
    {
        await PublishAsync(_options.DirectExchangeName, routingKey, payload);
    }

    public async Task PublishToTopicExchangeAsync(string routingKey, object payload)
    {
        await PublishAsync(_options.TopicExchangeName, routingKey, payload);
    }

    private async Task PublishAsync(string exchangeName, string routingKey, object payload)
    {
        var factory = new ConnectionFactory
        {
            HostName = _options.HostName,
            Port = _options.Port,
            UserName = _options.UserName,
            Password = _options.Password
        };

        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        var message = new MessageEnvelope
        {
            RoutingKey = routingKey,
            Payload = payload
        };

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        var properties = new BasicProperties
        {
            Persistent = true,
            ContentType = "application/json"
        };

        await channel.BasicPublishAsync(
            exchange: exchangeName,
            routingKey: routingKey,
            mandatory: false,
            basicProperties: properties,
            body: body);
    }
}