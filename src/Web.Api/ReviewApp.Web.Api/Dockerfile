FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Web.Api/ReviewApp.Web.Api/ReviewApp.Web.Api.csproj Web.Api/ReviewApp.Web.Api/
COPY Shared/ReviewApp.ServiceBusAdapter/ReviewApp.ServiceBusAdapter.csproj Shared/ReviewApp.ServiceBusAdapter/
COPY Shared/ReviewApp.ServiceBusAdapter.Abstractions/ReviewApp.ServiceBusAdapter.Abstractions.csproj Shared/ReviewApp.ServiceBusAdapter.Abstractions/
COPY Web.Api/ReviewApp.Web.DataAccess/ReviewApp.Web.DataAccess.csproj Web.Api/ReviewApp.Web.DataAccess/
COPY Web.Api/ReviewApp.Web.Core/ReviewApp.Web.Core.csproj Web.Api/ReviewApp.Web.Core/
COPY Web.Api/ReviewApp.Web.Services/ReviewApp.Web.Services.csproj Web.Api/ReviewApp.Web.Services/
COPY Shared/ReviewApp.TransferObjects/ReviewApp.TransferObjects.csproj Shared/ReviewApp.TransferObjects/
COPY Shared/ReviewApp.Utilities/ReviewApp.Utilities.csproj Shared/ReviewApp.Utilities/
RUN dotnet restore Web.Api/ReviewApp.Web.Api/ReviewApp.Web.Api.csproj
COPY . .
WORKDIR /src/Web.Api/ReviewApp.Web.Api
RUN dotnet build ReviewApp.Web.Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish ReviewApp.Web.Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ReviewApp.Web.Api.dll"]
