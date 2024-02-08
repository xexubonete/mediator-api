#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Base Stage: Use ASP.NET runtime as the base image, set user, working directory, and expose ports
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build Stage: Use .NET SDK as the build image, set build configuration, copy project, restore, copy source code, and build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["webapi-docker/webapi-docker.csproj", "webapi-docker/"]
RUN dotnet restore "./webapi-docker/./webapi-docker.csproj"
COPY . .
WORKDIR "/src/webapi-docker"
RUN dotnet build "./webapi-docker.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish Stage: Use build image, set build configuration, and publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./webapi-docker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final Stage: Use base image, set working directory, copy published application, and set entry point
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "webapi-docker.dll"]
