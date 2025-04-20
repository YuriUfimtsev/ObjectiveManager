FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ObjectiveManager.NotificationsService/NotificationsService.Api/NotificationsService.Api.csproj", "ObjectiveManager.NotificationsService/NotificationsService.Api/"]
RUN dotnet restore "ObjectiveManager.NotificationsService/NotificationsService.Api/NotificationsService.Api.csproj"
COPY . .
WORKDIR "/src/ObjectiveManager.NotificationsService/NotificationsService.Api"
RUN dotnet build "NotificationsService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "NotificationsService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 25
ENTRYPOINT ["dotnet", "NotificationsService.Api.dll"]
