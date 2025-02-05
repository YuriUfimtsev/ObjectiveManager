FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ObjectiveManager.APIGateway/APIGateway.Api/APIGateway.Api.csproj", "ObjectiveManager.APIGateway/APIGateway.Api/"]
COPY ["ObjectiveManager.ObjectivesService/ObjectivesService.Client/ObjectivesService.Client.csproj", "ObjectiveManager.ObjectivesService/ObjectivesService.Client/"]
COPY ["ObjectiveManager.Common/ObjectiveManager.Models/ObjectiveManager.Models.csproj", "ObjectiveManager.Common/ObjectiveManager.Models/"]
COPY ["ObjectiveManager.Common/ObjectiveManager.Utils/ObjectiveManager.Utils.csproj", "ObjectiveManager.Common/ObjectiveManager.Utils/"]
COPY ["ObjectiveManager.AuthService/AuthService.Client/AuthService.Client.csproj", "ObjectiveManager.AuthService/AuthService.Client/"]
COPY ["ObjectiveManager.NotificationsService/NotificationsService.Client/NotificationsService.Client.csproj", "ObjectiveManager.NotificationsService/NotificationsService.Client/"]
RUN dotnet restore "ObjectiveManager.APIGateway/APIGateway.Api/APIGateway.Api.csproj"
COPY . .
WORKDIR "/src/ObjectiveManager.APIGateway/APIGateway.Api"
RUN dotnet build "APIGateway.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "APIGateway.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

# Install curl for healthcheck
USER root
RUN apt-get update && apt-get install -y curl

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "APIGateway.Api.dll"]
