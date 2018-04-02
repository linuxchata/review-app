FROM microsoft/aspnetcore:2.0-nanoserver-sac2016 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0-nanoserver-sac2016 AS build
WORKDIR /src
COPY LC.RA.All.sln ./
COPY Services/Location/LC.RA.Location.Api/LC.RA.Location.Api.csproj Services/Location/LC.RA.Location.Api/
COPY Shared/LC.ServiceBusAdapter/LC.ServiceBusAdapter.csproj Shared/LC.ServiceBusAdapter/
COPY Shared/LC.ServiceBusAdapter.Abstractions/LC.ServiceBusAdapter.Abstractions.csproj Shared/LC.ServiceBusAdapter.Abstractions/
COPY Shared/LC.RA.TransferObjects/LC.RA.TransferObjects.csproj Shared/LC.RA.TransferObjects/
COPY Services/Location/LC.RA.Location.Core/LC.RA.Location.Core.csproj Services/Location/LC.RA.Location.Core/
COPY Services/Location/LC.RA.Location.Infrastructure/LC.RA.Location.Infrastructure.csproj Services/Location/LC.RA.Location.Infrastructure/
RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/Services/Location/LC.RA.Location.Api
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "LC.RA.Location.Api.dll"]
