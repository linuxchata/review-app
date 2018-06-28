FROM microsoft/aspnetcore:2.0-nanoserver-sac2016 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0-nanoserver-sac2016 AS build
WORKDIR /src
COPY . .
COPY /src/Web.Api/LC.RA.Web.sln ./
#COPY Web.Api/LC.RA.Web.Api/LC.RA.Web.Api.csproj Web.Api/LC.RA.Web.Api/
#COPY Shared/LC.ServiceBusAdapter.Abstractions/LC.ServiceBusAdapter.Abstractions.csproj Shared/LC.ServiceBusAdapter.Abstractions/
#COPY Web.Api/LC.RA.Web.Core/LC.RA.Web.Core.csproj Web.Api/LC.RA.Web.Core/
#COPY Web.Api/LC.RA.Web.Services/LC.RA.Web.Services.csproj Web.Api/LC.RA.Web.Services/
#COPY Shared/LC.RA.Utilities/LC.RA.Utilities.csproj Shared/LC.RA.Utilities/
#COPY Shared/LC.RA.TransferObjects/LC.RA.TransferObjects.csproj Shared/LC.RA.TransferObjects/
#COPY Web.Api/LC.RA.Web.DataAccess/LC.RA.Web.DataAccess.csproj Web.Api/LC.RA.Web.DataAccess/
#COPY Shared/LC.ServiceBusAdapter/LC.ServiceBusAdapter.csproj Shared/LC.ServiceBusAdapter/
#COPY . .
WORKDIR /src/Web.Api/LC.RA.Web.Api
RUN dotnet restore -nowarn:msb3202,nu1503
RUN dotnet build --no-restore -c Release -o /app

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "LC.RA.Web.Api.dll"]
