FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ObjectiveManager.ObjectivesService/ObjectivesService.Api/ObjectivesService.Api.csproj", "ObjectiveManager.ObjectivesService/ObjectivesService.Api/"]
COPY ["ObjectiveManager.ObjectivesService/ObjectivesService/ObjectivesService.Domain/ObjectivesService.Domain.csproj", "ObjectiveManager.ObjectivesService/ObjectivesService/ObjectivesService.Domain/"]
COPY ["ObjectiveManager.ObjectivesService/ObjectivesService/ObjectivesService.DataAccess/ObjectivesService.DataAccess.csproj", "ObjectiveManager.ObjectivesService/ObjectivesService/ObjectivesService.DataAccess/"]
COPY ["ObjectiveManager.ObjectivesService/ObjectivesService/ObjectivesService.Application/ObjectivesService.Application.csproj", "ObjectiveManager.ObjectivesService/ObjectivesService/ObjectivesService.Application/"]
COPY ["ObjectiveManager.Common/ObjectiveManager.Utils/ObjectiveManager.Utils.csproj", "ObjectiveManager.Common/ObjectiveManager.Utils/"]
COPY ["ObjectiveManager.Common/ObjectiveManager.Models/ObjectiveManager.Models.csproj", "ObjectiveManager.Common/ObjectiveManager.Models/"]
COPY ["ObjectiveManager.AuthService/AuthService.Client/AuthService.Client.csproj", "ObjectiveManager.AuthService/AuthService.Client/"]
RUN dotnet restore "ObjectiveManager.ObjectivesService/ObjectivesService.Api/ObjectivesService.Api.csproj"
COPY . .
WORKDIR "/src/ObjectiveManager.ObjectivesService/ObjectivesService.Api"
RUN dotnet build "ObjectivesService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ObjectivesService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ObjectivesService.Api.dll"]