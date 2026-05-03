using Lab4.Contracts;
using Lab4.RabbitMq;
using Microsoft.AspNetCore.Mvc;

namespace Lab4.Api.Controllers;

[ApiController]
[Route("api/patient-events")]
public sealed class PatientEventsController : ControllerBase
{
    private readonly IRabbitMqPublisher _publisher;

    public PatientEventsController(IRabbitMqPublisher publisher)
    {
        _publisher = publisher;
    }

    [HttpPost]
    public async Task<IActionResult> Publish([FromBody] PatientEventRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.PatientId))
        {
            return BadRequest("Patient id is required.");
        }

        if (string.IsNullOrWhiteSpace(request.EventType))
        {
            return BadRequest("Event type is required.");
        }

        if (!RabbitMqTopology.PatientIds.Contains(request.PatientId))
        {
            return BadRequest($"Unsupported patient id: {request.PatientId}");
        }

        if (!RabbitMqTopology.PatientEventTypes.Contains(request.EventType))
        {
            return BadRequest($"Unsupported patient event type: {request.EventType}");
        }

        var routingKey = $"patient.{request.PatientId}.{request.EventType}";

        await _publisher.PublishToTopicExchangeAsync(
            routingKey,
            request.Payload);

        return Accepted(new
        {
            message = "Patient event was published.",
            routingKey
        });
    }
}