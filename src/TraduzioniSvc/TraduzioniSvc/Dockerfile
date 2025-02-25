FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/TraduzioniSvc/TraduzioniSvc/TraduzioniSvc.csproj", "src/TraduzioniSvc/TraduzioniSvc/"]
COPY ["src/TraduzioniSvc/TraduzioniSvc.BusinessLayer/TraduzioniSvc.BusinessLayer.csproj", "src/TraduzioniSvc/TraduzioniSvc.BusinessLayer/"]
#COPY ["src/TraduzioniSvc/TraduzioniSvc.BusinessLayer.Test/TraduzioniSvc.BusinessLayer.Test.csproj", "src/TraduzioniSvc/TraduzioniSvc.BusinessLayer.Test/"]
#COPY ["src/TraduzioniSvc/TraduzioniSvc.DataAccessLayer/TraduzioniSvc.DataAccessLayer.csproj", "src/TraduzioniSvc/TraduzioniSvc.DataAccessLayer/"]
#COPY ["src/TraduzioniSvc/TraduzioniSvc.IntegrationTest/TraduzioniSvc.IntegrationTest.csproj", "src/TraduzioniSvc/TraduzioniSvc.IntegrationTest/"]
#COPY ["src/TraduzioniSvc/TraduzioniSvc.Migrations/TraduzioniSvc.Migrations.csproj", "src/TraduzioniSvc/TraduzioniSvc.Migrations/"]
COPY ["src/TraduzioniSvc/TraduzioniSvc.Shared/TraduzioniSvc.Shared.csproj", "src/TraduzioniSvc/TraduzioniSvc.Shared/"]
COPY ["src/GSWCloudApp.Common/GSWCloudApp.Common.csproj", "src/GSWCloudApp.Common/"]

RUN dotnet restore "src/TraduzioniSvc/TraduzioniSvc/TraduzioniSvc.csproj"
COPY . .

WORKDIR "/src/src/TraduzioniSvc/TraduzioniSvc"
RUN dotnet build "TraduzioniSvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TraduzioniSvc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TraduzioniSvc.dll"]