FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Catalog/Microservices.Catalog.API/Microservices.Catalog.API.csproj", "src/Catalog/Microservices.Catalog.API/"]
RUN dotnet restore "src/Catalog/Microservices.Catalog.API/Microservices.Catalog.API.csproj"
COPY . .
WORKDIR "/src/src/Catalog/Microservices.Catalog.API"
RUN dotnet build "Microservices.Catalog.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservices.Catalog.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservices.Catalog.API.dll"]
