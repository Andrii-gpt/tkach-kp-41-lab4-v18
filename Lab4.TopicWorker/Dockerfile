FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Lab4.TopicWorker/Lab4.TopicWorker.csproj", "Lab4.TopicWorker/"]
COPY ["Lab4.Contracts/Lab4.Contracts.csproj", "Lab4.Contracts/"]
COPY ["Lab4.RabbitMq/Lab4.RabbitMq.csproj", "Lab4.RabbitMq/"]
RUN dotnet restore "Lab4.TopicWorker/Lab4.TopicWorker.csproj"

COPY . .
WORKDIR "/src/Lab4.TopicWorker"
RUN dotnet publish "Lab4.TopicWorker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Lab4.TopicWorker.dll"]