FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["InfotecsTestCase/InfotecsTestCase.csproj", "InfotecsTestCase/"]
RUN dotnet restore "InfotecsTestCase/InfotecsTestCase.csproj"
COPY . .
WORKDIR "/src/InfotecsTestCase"
RUN dotnet build "./InfotecsTestCase.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./InfotecsTestCase.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InfotecsTestCase.dll"]
