FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/ConfigurazioniSvc/ConfigurazioniSvc/ConfigurazioniSvc.csproj", "src/ConfigurazioniSvc/ConfigurazioniSvc/"]
COPY ["src/ConfigurazioniSvc/ConfigurazioniSvc.BusinessLayer/ConfigurazioniSvc.BusinessLayer.csproj", "src/ConfigurazioniSvc/ConfigurazioniSvc.BusinessLayer/"]
#COPY ["src/ConfigurazioniSvc/ConfigurazioniSvc.BusinessLayer.Test/ConfigurazioniSvc.BusinessLayer.Test.csproj", "src/ConfigurazioniSvc/ConfigurazioniSvc.BusinessLayer.Test/"]
#COPY ["src/ConfigurazioniSvc/ConfigurazioniSvc.DataAccessLayer/ConfigurazioniSvc.DataAccessLayer.csproj", "src/ConfigurazioniSvc/ConfigurazioniSvc.DataAccessLayer/"]
#COPY ["src/ConfigurazioniSvc/ConfigurazioniSvc.IntegrationTest/ConfigurazioniSvc.IntegrationTest.csproj", "src/ConfigurazioniSvc/ConfigurazioniSvc.IntegrationTest/"]
#COPY ["src/ConfigurazioniSvc/ConfigurazioniSvc.Migrations/ConfigurazioniSvc.Migrations.csproj", "src/ConfigurazioniSvc/ConfigurazioniSvc.Migrations/"]
COPY ["src/ConfigurazioniSvc/ConfigurazioniSvc.Shared/ConfigurazioniSvc.Shared.csproj", "src/ConfigurazioniSvc/ConfigurazioniSvc.Shared/"]
COPY ["src/GSWCloudApp.Common/GSWCloudApp.Common.csproj", "src/GSWCloudApp.Common/"]

RUN dotnet restore "src/ConfigurazioniSvc/ConfigurazioniSvc/ConfigurazioniSvc.csproj"
COPY . .

WORKDIR "/src/src/ConfigurazioniSvc/ConfigurazioniSvc"
RUN dotnet build "ConfigurazioniSvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConfigurazioniSvc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConfigurazioniSvc.dll"]