FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/services/BlogService/BlogService.API/BlogService.API.csproj", "src/services/BlogService/BlogService.API/"]
COPY ["src/services/BlogService/BlogService.Application/BlogService.Application.csproj", "src/services/BlogService/BlogService.Application/"]
COPY ["src/services/BlogService/BlogService.Domain/BlogService.Domain.csproj", "src/services/BlogService/BlogService.Domain/"]
COPY ["src/services/BlogService/BlogService.Infrastructure/BlogService.Infrastructure.csproj", "src/services/BlogService/BlogService.Infrastructure/"]
COPY ["src/services/BlogService/BlogService.Persistence/BlogService.Persistence.csproj", "src/services/BlogService/BlogService.Persistence/"]
# Eğer Shared projelerden referans varsa onları da ekle:
COPY ["src/services/Shared/SharedEvents/SharedEvents.csproj", "src/services/Shared/SharedEvents/"]
RUN dotnet restore "src/services/BlogService/BlogService.API/BlogService.API.csproj"
COPY . .
WORKDIR "/src/src/services/BlogService/BlogService.API"
RUN dotnet build "BlogService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BlogService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlogService.API.dll"]