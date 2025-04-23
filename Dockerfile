FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Tasks.API/Tasks.API.csproj", "Tasks.API/"]
COPY ["Tasks.App.Servicos/Tasks.App.Servicos.csproj", "./"]
COPY ["Tasks.DataTransfer/Tasks.DataTransfer.csproj", "./"]
COPY ["Tasks.Dominios/Tasks.Dominios.csproj", "./"]
COPY ["Tasks.Infra/Tasks.Infra.csproj", "./"]
RUN dotnet restore "Tasks.API/Tasks.API.csproj"
COPY . .
WORKDIR "/src/Tasks.API"
RUN dotnet build "Tasks.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Tasks.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tasks.API.dll"]

EXPOSE 5000
EXPOSE 5001
