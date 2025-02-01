FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ObjectiveManager.AuthService/AuthService.Api/AuthService.Api.csproj", "ObjectiveManager.AuthService/AuthService.Api/"]
COPY ["ObjectiveManager.AuthService/AuthService/AuthService.Application/AuthService.Application.csproj", "ObjectiveManager.AuthService/AuthService/AuthService.Application/"]
COPY ["ObjectiveManager.AuthService/AuthService/AuthService.Domain/AuthService.Domain.csproj", "ObjectiveManager.AuthService/AuthService/AuthService.Domain/"]
COPY ["ObjectiveManager.AuthService/AuthService/AuthService.DataAccess/AuthService.DataAccess.csproj", "ObjectiveManager.AuthService/AuthService/AuthService.DataAccess/"]
COPY ["ObjectiveManager.Common/ObjectiveManager.Utils/ObjectiveManager.Utils.csproj", "ObjectiveManager.Common/ObjectiveManager.Utils/"]
COPY ["ObjectiveManager.Common/ObjectiveManager.Models/ObjectiveManager.Models.csproj", "ObjectiveManager.Common/ObjectiveManager.Models/"]
RUN dotnet restore "ObjectiveManager.AuthService/AuthService.Api/AuthService.Api.csproj"
COPY . .
WORKDIR "/src/ObjectiveManager.AuthService/AuthService.Api"
RUN dotnet build "AuthService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AuthService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthService.Api.dll"]
