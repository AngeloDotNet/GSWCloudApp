FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/AutenticazioneSvc/AutenticazioneSvc/AutenticazioneSvc.csproj", "src/AutenticazioneSvc/AutenticazioneSvc/"]
COPY ["src/AutenticazioneSvc/AutenticazioneSvc.BusinessLayer/AutenticazioneSvc.BusinessLayer.csproj", "src/AutenticazioneSvc/AutenticazioneSvc.BusinessLayer/"]
COPY ["src/AutenticazioneSvc/AutenticazioneSvc.BusinessLayer.Test/AutenticazioneSvc.BusinessLayer.Test.csproj", "src/AutenticazioneSvc/AutenticazioneSvc.BusinessLayer.Test/"]
COPY ["src/AutenticazioneSvc/AutenticazioneSvc.DataAccessLayer/AutenticazioneSvc.DataAccessLayer.csproj", "src/AutenticazioneSvc/AutenticazioneSvc.DataAccessLayer/"]
COPY ["src/AutenticazioneSvc/AutenticazioneSvc.IntegrationTest/AutenticazioneSvc.IntegrationTest.csproj", "src/AutenticazioneSvc/AutenticazioneSvc.IntegrationTest/"]
COPY ["src/AutenticazioneSvc/AutenticazioneSvc.Migrations/AutenticazioneSvc.Migrations.csproj", "src/AutenticazioneSvc/AutenticazioneSvc.Migrations/"]
COPY ["src/AutenticazioneSvc/AutenticazioneSvc.Shared/AutenticazioneSvc.Shared.csproj", "src/AutenticazioneSvc/AutenticazioneSvc.Shared/"]

RUN dotnet restore "src/AutenticazioneSvc/AutenticazioneSvc/AutenticazioneSvc.csproj"
COPY . .

WORKDIR "/src/src/AutenticazioneSvc/AutenticazioneSvc"
RUN dotnet build "AutenticazioneSvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AutenticazioneSvc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AutenticazioneSvc.dll"]