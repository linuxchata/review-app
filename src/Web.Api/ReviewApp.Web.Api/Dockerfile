FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 443

WORKDIR /etc/ssl/certs/
COPY install/https/dev.reviewapp.com.pfx
COPY install/https/reviewapp-dev-ca.pem

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["src/Web.Api/ReviewApp.Web.Api/ReviewApp.Web.Api.csproj", "src/Web.Api/ReviewApp.Web.Api/"]
COPY ["src/Shared/ReviewApp.HealthChecks/ReviewApp.HealthChecks.csproj", "src/Shared/ReviewApp.HealthChecks/"]
COPY ["src/Shared/ReviewApp.ServiceBusAdapter/ReviewApp.ServiceBusAdapter.csproj", "src/Shared/ReviewApp.ServiceBusAdapter/"]
COPY ["src/Shared/ReviewApp.ServiceBusAdapter.Abstractions/ReviewApp.ServiceBusAdapter.Abstractions.csproj", "src/Shared/ReviewApp.ServiceBusAdapter.Abstractions/"]
COPY ["src/Web.Api/ReviewApp.Web.DataAccess/ReviewApp.Web.DataAccess.csproj", "src/Web.Api/ReviewApp.Web.DataAccess/"]
COPY ["src/Web.Api/ReviewApp.Web.Core/ReviewApp.Web.Core.csproj", "src/Web.Api/ReviewApp.Web.Core/"]
COPY ["src/Web.Api/ReviewApp.Web.Services/ReviewApp.Web.Services.csproj", "src/Web.Api/ReviewApp.Web.Services/"]
COPY ["src/Shared/ReviewApp.TransferObjects/ReviewApp.TransferObjects.csproj", "src/Shared/ReviewApp.TransferObjects/"]
COPY ["src/Shared/ReviewApp.Utilities/ReviewApp.Utilities.csproj", "src/Shared/ReviewApp.Utilities/"]
RUN dotnet restore "src/Web.Api/ReviewApp.Web.Api/ReviewApp.Web.Api.csproj"
COPY . .
WORKDIR "/src/src/Web.Api/ReviewApp.Web.Api"
RUN dotnet build "ReviewApp.Web.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ReviewApp.Web.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ReviewApp.Web.Api.dll"]