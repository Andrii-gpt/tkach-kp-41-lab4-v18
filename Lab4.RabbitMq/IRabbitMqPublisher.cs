namespace Lab4.RabbitMq;

public interface IRabbitMqPublisher
{
    Task PublishToDirectExchangeAsync(string routingKey, object payload);

    Task PublishToTopicExchangeAsync(string routingKey, object payload);
}