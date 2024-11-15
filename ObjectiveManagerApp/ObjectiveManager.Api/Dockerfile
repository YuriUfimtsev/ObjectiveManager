FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ObjectiveManager.Api/ObjectiveManager.Api.csproj", "ObjectiveManager.Api/"]
COPY ["ObjectiveManager.Application/ObjectiveManager.Application.csproj", "ObjectiveManager.Application/"]
COPY ["ObjectiveManager.DataAccess/ObjectiveManager.DataAccess.csproj", "ObjectiveManager.DataAccess/"]
COPY ["ObjectiveManager.Domain/ObjectiveManager.Domain.csproj", "ObjectiveManager.Domain/"]
RUN dotnet restore "ObjectiveManager.Api/ObjectiveManager.Api.csproj"
COPY . .
WORKDIR "/src/ObjectiveManager.Api"
RUN dotnet build "ObjectiveManager.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ObjectiveManager.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

# Ensure directory exists and copy the necessary CSV file into the container
RUN mkdir -p /Data
COPY Data/Objectives.csv /Data/

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ObjectiveManager.Api.dll"]
