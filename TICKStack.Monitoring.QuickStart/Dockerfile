FROM mcr.microsoft.com/dotnet/core/runtime:2.2-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["TICKStack.Monitoring.QuickStart/TICKStack.Monitoring.QuickStart.csproj", "./"]
RUN dotnet restore "./TICKStack.Monitoring.QuickStart.csproj"

COPY ./TICKStack.Monitoring.QuickStart/ .

RUN dotnet build "TICKStack.Monitoring.QuickStart.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TICKStack.Monitoring.QuickStart.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TICKStack.Monitoring.QuickStart.dll"]