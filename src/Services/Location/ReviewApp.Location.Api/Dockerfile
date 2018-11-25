FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Services/Location/ReviewApp.Location.Api/ReviewApp.Location.Api.csproj Services/Location/ReviewApp.Location.Api/
COPY Shared/ReviewApp.ServiceBusAdapter.Abstractions/ReviewApp.ServiceBusAdapter.Abstractions.csproj Shared/ReviewApp.ServiceBusAdapter.Abstractions/
COPY Shared/ReviewApp.ServiceBusAdapter/ReviewApp.ServiceBusAdapter.csproj Shared/ReviewApp.ServiceBusAdapter/
COPY Shared/ReviewApp.TransferObjects/ReviewApp.TransferObjects.csproj Shared/ReviewApp.TransferObjects/
COPY Services/Location/ReviewApp.Location.Infrastructure/ReviewApp.Location.Infrastructure.csproj Services/Location/ReviewApp.Location.Infrastructure/
COPY Services/Location/ReviewApp.Location.Core/ReviewApp.Location.Core.csproj Services/Location/ReviewApp.Location.Core/
RUN dotnet restore Services/Location/ReviewApp.Location.Api/ReviewApp.Location.Api.csproj
COPY . .
WORKDIR /src/Services/Location/ReviewApp.Location.Api
RUN dotnet build ReviewApp.Location.Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish ReviewApp.Location.Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ReviewApp.Location.Api.dll"]
