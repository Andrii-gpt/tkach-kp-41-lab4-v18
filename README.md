Event-Driven Messaging System with ASP.NET Core & RabbitMQ
A distributed messaging application demonstrating Direct and Topic RabbitMQ exchanges.
The system exposes REST endpoints for publishing tracking and patient events and routes them asynchronously to dedicated worker services.
Client
   ↓
ASP.NET Core API
   ↓
RabbitMQ
   ├── Direct Exchange → Tracking Workers
   └── Topic Exchange  → Patient Event Workers
