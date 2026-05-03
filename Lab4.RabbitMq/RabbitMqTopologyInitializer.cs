using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Lab4.RabbitMq;

public sealed class RabbitMqTopologyInitializer
{
    private readonly RabbitMqOptions _options;

    public RabbitMqTopologyInitializer(IOptions<RabbitMqOptions> options)
    {
        _options = options.Value;
    }

    public async Task InitializeAsync()
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

        await DeclareDirectTopologyAsync(channel);
        await DeclareTopicTopologyAsync(channel);
    }

    private async Task DeclareDirectTopologyAsync(IChannel channel)
    {
        await channel.ExchangeDeclareAsync(
            exchange: _options.DirectExchangeName,
            type: ExchangeType.Direct,
            durable: true);

        foreach (var queueBinding in RabbitMqTopology.DirectQueueBindings)
        {
            var queueName = queueBinding.Key;
            var routingKeys = queueBinding.Value;

            await channel.QueueDeclareAsync(
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false);

            foreach (var routingKey in routingKeys)
            {
                await channel.QueueBindAsync(
                    queue: queueName,
                    exchange: _options.DirectExchangeName,
                    routingKey: routingKey);
            }
        }
    }

    private async Task DeclareTopicTopologyAsync(IChannel channel)
    {
        await channel.ExchangeDeclareAsync(
            exchange: _options.TopicExchangeName,
            type: ExchangeType.Topic,
            durable: true);

        foreach (var queueBinding in RabbitMqTopology.PatientQueueBindings)
        {
            var queueName = queueBinding.Key;
            var bindingKey = queueBinding.Value;

            await channel.QueueDeclareAsync(
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false);

            await channel.QueueBindAsync(
                queue: queueName,
                exchange: _options.TopicExchangeName,
                routingKey: bindingKey);
        }
    }
}