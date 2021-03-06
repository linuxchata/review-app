FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 443

WORKDIR /etc/ssl/certs/
COPY install/https/dev.reviewapp.com.pfx dev.reviewapp.com.pfx
COPY install/https/reviewapp-dev-ca.pem reviewapp-dev-ca.pem

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
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