FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/services/NotificationService/NotificationService.API/NotificationService.API.csproj", "src/services/NotificationService/NotificationService.API/"]
COPY ["src/services/NotificationService/NotificationService.Application/NotificationService.Application.csproj", "src/services/NotificationService/NotificationService.Application/"]
COPY ["src/services/NotificationService/NotificationService.Domain/NotificationService.Domain.csproj", "src/services/NotificationService/NotificationService.Domain/"]
COPY ["src/services/NotificationService/NotificationService.Infrastructure/NotificationService.Infrastructure.csproj", "src/services/NotificationService/NotificationService.Infrastructure/"]
COPY ["src/services/NotificationService/NotificationService.Persistance/NotificationService.Persistance.csproj", "src/services/NotificationService/NotificationService.Persistance/"]
# Eğer Shared projelerden referans varsa onları da ekle:
COPY ["src/services/Shared/SharedEvents/SharedEvents.csproj", "src/services/Shared/SharedEvents/"]
RUN dotnet restore "src/services/NotificationService/NotificationService.API/NotificationService.API.csproj"
COPY . .
WORKDIR "/src/src/services/NotificationService/NotificationService.API"
RUN dotnet build "NotificationService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "NotificationService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotificationService.API.dll"]