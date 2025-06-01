FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/GestioneLoghiSvc/GestioneLoghiSvc/GestioneLoghiSvc.csproj", "src/GestioneLoghiSvc/GestioneLoghiSvc/"]
COPY ["src/GestioneLoghiSvc/GestioneLoghiSvc.BusinessLayer/GestioneLoghiSvc.BusinessLayer.csproj", "src/GestioneLoghiSvc/GestioneLoghiSvc.BusinessLayer/"]
#COPY ["src/GestioneLoghiSvc/GestioneLoghiSvc.BusinessLayer.Test/GestioneLoghiSvc.BusinessLayer.Test.csproj", "src/GestioneLoghiSvc/GestioneLoghiSvc.BusinessLayer.Test/"]
COPY ["src/GestioneLoghiSvc/GestioneLoghiSvc.DataAccessLayer/GestioneLoghiSvc.DataAccessLayer.csproj", "src/GestioneLoghiSvc/GestioneLoghiSvc.DataAccessLayer/"]
#COPY ["src/GestioneLoghiSvc/GestioneLoghiSvc.IntegrationTest/GestioneLoghiSvc.IntegrationTest.csproj", "src/GestioneLoghiSvc/GestioneLoghiSvc.IntegrationTest/"]
COPY ["src/GestioneLoghiSvc/GestioneLoghiSvc.Migrations/GestioneLoghiSvc.Migrations.csproj", "src/GestioneLoghiSvc/GestioneLoghiSvc.Migrations/"]
COPY ["src/GestioneLoghiSvc/GestioneLoghiSvc.Shared/GestioneLoghiSvc.Shared.csproj", "src/GestioneLoghiSvc/GestioneLoghiSvc.Shared/"]
COPY ["src/GSWCloudApp.Common/GSWCloudApp.Common.csproj", "src/GSWCloudApp.Common/"]

RUN dotnet restore "src/GestioneLoghiSvc/GestioneLoghiSvc/GestioneLoghiSvc.csproj"
COPY . .

WORKDIR "/src/src/GestioneLoghiSvc/GestioneLoghiSvc"
RUN dotnet build "GestioneLoghiSvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GestioneLoghiSvc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GestioneLoghiSvc.dll"]