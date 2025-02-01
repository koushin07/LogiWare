#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Logiware.API/Logiware.API.csproj", "Logiware.API/"]
COPY ["Logiware.Application/Logiware.Application.csproj", "Logiware.Application/"]
COPY ["Logiware.Domain/Logiware.Domain.csproj", "Logiware.Domain/"]
COPY ["Logiware.Infrastructure/Logiware.Infrastructure.csproj", "Logiware.Infrastructure/"]
RUN dotnet restore "./Logiware.API/Logiware.API.csproj"
COPY . .
WORKDIR "/src/Logiware.API"
RUN dotnet build "./Logiware.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Logiware.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Logiware.API.dll"]