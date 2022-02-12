FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Services/Location/ReviewApp.Location.Api/ReviewApp.Location.Api.csproj", "src/Services/Location/ReviewApp.Location.Api/"]
COPY ["src/Shared/ReviewApp.HealthChecks/ReviewApp.HealthChecks.csproj", "src/Shared/ReviewApp.HealthChecks/"]
COPY ["src/Shared/ReviewApp.ServiceBusAdapter.Abstractions/ReviewApp.ServiceBusAdapter.Abstractions.csproj", "src/Shared/ReviewApp.ServiceBusAdapter.Abstractions/"]
COPY ["src/Shared/ReviewApp.ServiceBusAdapter/ReviewApp.ServiceBusAdapter.csproj", "src/Shared/ReviewApp.ServiceBusAdapter/"]
COPY ["src/Shared/ReviewApp.Api.Infrastructure/ReviewApp.Api.Infrastructure.csproj", "src/Shared/ReviewApp.Api.Infrastructure/"]
COPY ["src/Shared/ReviewApp.TransferObjects/ReviewApp.TransferObjects.csproj", "src/Shared/ReviewApp.TransferObjects/"]
COPY ["src/Services/Location/ReviewApp.Location.Infrastructure/ReviewApp.Location.Infrastructure.csproj", "src/Services/Location/ReviewApp.Location.Infrastructure/"]
COPY ["src/Services/Location/ReviewApp.Location.Core/ReviewApp.Location.Core.csproj", "src/Services/Location/ReviewApp.Location.Core/"]
RUN dotnet restore "src/Services/Location/ReviewApp.Location.Api/ReviewApp.Location.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Location/ReviewApp.Location.Api"
RUN dotnet build "ReviewApp.Location.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReviewApp.Location.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReviewApp.Location.Api.dll"]