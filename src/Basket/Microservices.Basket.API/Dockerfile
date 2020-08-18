FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Basket/Microservices.Basket.API/Microservices.Basket.API.csproj", "src/Basket/Microservices.Basket.API/"]
RUN dotnet restore "src/Basket/Microservices.Basket.API/Microservices.Basket.API.csproj"
COPY . .
WORKDIR "/src/src/Basket/Microservices.Basket.API"
RUN dotnet build "Microservices.Basket.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservices.Basket.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservices.Basket.API.dll"]
