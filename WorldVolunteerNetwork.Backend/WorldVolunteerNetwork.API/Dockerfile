#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WorldVolunteerNetwork.API/WorldVolunteerNetwork.API.csproj", "WorldVolunteerNetwork.API/"]
COPY ["WorldVolunteerNetwork.Infrastructure/WorldVolunteerNetwork.Infrastructure.csproj", "WorldVolunteerNetwork.Infrastructure/"]
COPY ["WorldVolunteerNetwork.Application/WorldVolunteerNetwork.Application.csproj", "WorldVolunteerNetwork.Application/"]
COPY ["WorldVolunteerNetwork.Domain/WorldVolunteerNetwork.Domain.csproj", "WorldVolunteerNetwork.Domain/"]
RUN dotnet restore "./WorldVolunteerNetwork.API/WorldVolunteerNetwork.API.csproj"
COPY . .
WORKDIR "/src/WorldVolunteerNetwork.API"
RUN dotnet build "./WorldVolunteerNetwork.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WorldVolunteerNetwork.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorldVolunteerNetwork.API.dll"]