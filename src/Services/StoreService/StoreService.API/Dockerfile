# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5002
EXPOSE 5003


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/StoreService/StoreService.API/StoreService.API.csproj", "Services/StoreService/StoreService.API/"]
COPY ["Shared/Shared/Shared.csproj", "Shared/Shared/"]
COPY ["Services/StoreService/StoreService.Application/StoreService.Application.csproj", "Services/StoreService/StoreService.Application/"]
COPY ["Services/StoreService/StoreService.Core/StoreService.Core.csproj", "Services/StoreService/StoreService.Core/"]
COPY ["Services/StoreService/StoreService.Infrastructure/StoreService.Infrastructure.csproj", "Services/StoreService/StoreService.Infrastructure/"]
COPY ["EventBus/EventBus/EventBus.csproj", "EventBus/EventBus/"]
RUN dotnet restore "./Services/StoreService/StoreService.API/StoreService.API.csproj"
COPY . .
WORKDIR "/src/Services/StoreService/StoreService.API"
RUN dotnet build "./StoreService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./StoreService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StoreService.API.dll"]