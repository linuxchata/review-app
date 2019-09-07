FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 443

WORKDIR /etc/ssl/certs/
COPY install/https/dev.reviewapp.com.pfx dev.reviewapp.com.pfx
COPY install/https/reviewapp-dev-ca.pem reviewapp-dev-ca.pem

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["src/Services/Location/ReviewApp.Location.Api/ReviewApp.Location.Api.csproj", "src/Services/Location/ReviewApp.Location.Api/"]
COPY ["src/Shared/ReviewApp.HealthChecks/ReviewApp.HealthChecks.csproj", "src/Shared/ReviewApp.HealthChecks/"]
COPY ["src/Shared/ReviewApp.ServiceBusAdapter.Abstractions/ReviewApp.ServiceBusAdapter.Abstractions.csproj", "src/Shared/ReviewApp.ServiceBusAdapter.Abstractions/"]
COPY ["src/Shared/ReviewApp.ServiceBusAdapter/ReviewApp.ServiceBusAdapter.csproj", "src/Shared/ReviewApp.ServiceBusAdapter/"]
COPY ["src/Shared/ReviewApp.TransferObjects/ReviewApp.TransferObjects.csproj", "src/Shared/ReviewApp.TransferObjects/"]
COPY ["src/Services/Location/ReviewApp.Location.Infrastructure/ReviewApp.Location.Infrastructure.csproj", "src/Services/Location/ReviewApp.Location.Infrastructure/"]
COPY ["src/Services/Location/ReviewApp.Location.Core/ReviewApp.Location.Core.csproj", "src/Services/Location/ReviewApp.Location.Core/"]
RUN dotnet restore "src/Services/Location/ReviewApp.Location.Api/ReviewApp.Location.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Location/ReviewApp.Location.Api"
RUN dotnet build "ReviewApp.Location.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ReviewApp.Location.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ReviewApp.Location.Api.dll"]