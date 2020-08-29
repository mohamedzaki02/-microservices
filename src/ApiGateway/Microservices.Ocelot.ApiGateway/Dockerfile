FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["src/ApiGateway/Microservices.Ocelot.ApiGateway/Microservices.Ocelot.ApiGateway.csproj", "src/ApiGateway/Microservices.Ocelot.ApiGateway/"]
RUN dotnet restore "src/ApiGateway/Microservices.Ocelot.ApiGateway/Microservices.Ocelot.ApiGateway.csproj"
COPY . .
WORKDIR "/src/src/ApiGateway/Microservices.Ocelot.ApiGateway"
RUN dotnet build "Microservices.Ocelot.ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservices.Ocelot.ApiGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservices.Ocelot.ApiGateway.dll"]
