namespace Lab4.RabbitMq;

public static class RabbitMqTopology
{
    public static readonly string[] DirectRoutingKeys =
    [
        "tracking:package",
        "tracking:shipment",
        "tracking:delivery",
        "tracking:return",
        "tracking"
    ];

    public static readonly Dictionary<string, string[]> DirectQueueBindings = new()
    {
        ["tracking.package.queue"] = ["tracking:package"],
        ["tracking.shipment.queue"] = ["tracking:shipment", "tracking"],
        ["tracking.delivery.queue"] = ["tracking:delivery"],
        ["tracking.return.queue"] = ["tracking:return"]
    };

    public static readonly Dictionary<string, string> PatientQueueBindings = new()
    {
        ["patient.p001.queue"] = "patient.p001.*",
        ["patient.p002.queue"] = "patient.p002.*",
        ["patient.p003.queue"] = "patient.p003.*"
    };

    public static readonly string[] PatientIds =
    [
        "p001",
        "p002",
        "p003"
    ];

    public static readonly string[] PatientEventTypes =
    [
        "appointment",
        "prescription",
        "test-result"
    ];
}