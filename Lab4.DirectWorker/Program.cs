using Lab4.DirectWorker;
using Lab4.RabbitMq;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddRabbitMqServices(builder.Configuration);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();

var rabbitMqInitializer = host.Services.GetRequiredService<RabbitMqTopologyInitializer>();
await rabbitMqInitializer.InitializeAsync();

host.Run();