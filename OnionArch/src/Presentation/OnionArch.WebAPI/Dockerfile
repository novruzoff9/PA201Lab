# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Presentation/OnionArch.WebAPI/OnionArch.WebAPI.csproj", "src/Presentation/OnionArch.WebAPI/"]
COPY ["src/Infrastructure/OnionArch.Infrastructure/OnionArch.Infrastructure.csproj", "src/Infrastructure/OnionArch.Infrastructure/"]
COPY ["src/Infrastructure/OnionArch.Persistance/OnionArch.Persistance.csproj", "src/Infrastructure/OnionArch.Persistance/"]
COPY ["src/Core/OnionArch.Application/OnionArch.Application.csproj", "src/Core/OnionArch.Application/"]
COPY ["src/Core/OnionArch.Domain/OnionArch.Domain.csproj", "src/Core/OnionArch.Domain/"]
RUN dotnet restore "./src/Presentation/OnionArch.WebAPI/OnionArch.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/Presentation/OnionArch.WebAPI"
RUN dotnet build "./OnionArch.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OnionArch.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OnionArch.WebAPI.dll"]