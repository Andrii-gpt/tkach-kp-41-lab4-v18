FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Lab4.Api/Lab4.Api.csproj", "Lab4.Api/"]
COPY ["Lab4.Contracts/Lab4.Contracts.csproj", "Lab4.Contracts/"]
COPY ["Lab4.RabbitMq/Lab4.RabbitMq.csproj", "Lab4.RabbitMq/"]
RUN dotnet restore "Lab4.Api/Lab4.Api.csproj"

COPY . .
WORKDIR "/src/Lab4.Api"
RUN dotnet publish "Lab4.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Lab4.Api.dll"]