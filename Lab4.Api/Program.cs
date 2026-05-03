using Lab4.RabbitMq;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRabbitMqServices(builder.Configuration);

var app = builder.Build();

var rabbitMqInitializer = app.Services.GetRequiredService<RabbitMqTopologyInitializer>();
await rabbitMqInitializer.InitializeAsync();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();