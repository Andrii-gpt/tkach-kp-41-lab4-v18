using Lab4.Contracts;
using Lab4.RabbitMq;
using Microsoft.AspNetCore.Mvc;

namespace Lab4.Api.Controllers;

[ApiController]
[Route("api/tracking-messages")]
public sealed class TrackingMessagesController : ControllerBase
{
    private readonly IRabbitMqPublisher _publisher;

    public TrackingMessagesController(IRabbitMqPublisher publisher)
    {
        _publisher = publisher;
    }

    [HttpPost]
    public async Task<IActionResult> Publish([FromBody] TrackingMessageRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.RoutingKey))
        {
            return BadRequest("Routing key is required.");
        }

        if (!RabbitMqTopology.DirectRoutingKeys.Contains(request.RoutingKey))
        {
            return BadRequest($"Unsupported routing key: {request.RoutingKey}");
        }

        await _publisher.PublishToDirectExchangeAsync(
            request.RoutingKey,
            request.Payload);

        return Accepted(new
        {
            message = "Tracking message was published.",
            request.RoutingKey
        });
    }
}