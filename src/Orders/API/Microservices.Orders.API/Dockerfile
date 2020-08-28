FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Orders/API/Microservices.Orders.API/Microservices.Orders.API.csproj", "src/Orders/API/Microservices.Orders.API/"]
RUN dotnet restore "src/Orders/API/Microservices.Orders.API/Microservices.Orders.API.csproj"
COPY . .
WORKDIR "/src/src/Orders/API/Microservices.Orders.API"
RUN dotnet build "Microservices.Orders.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservices.Orders.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservices.Orders.API.dll"]
