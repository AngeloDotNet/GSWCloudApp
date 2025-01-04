FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/GestioneDocumentiSvc/GestioneDocumentiSvc/GestioneDocumentiSvc.csproj", "src/GestioneDocumentiSvc/GestioneDocumentiSvc/"]
COPY ["src/GestioneDocumentiSvc/GestioneDocumentiSvc.BusinessLayer/GestioneDocumentiSvc.BusinessLayer.csproj", "src/GestioneDocumentiSvc/GestioneDocumentiSvc.BusinessLayer/"]
#COPY ["src/GestioneDocumentiSvc/GestioneDocumentiSvc.BusinessLayer.Test/GestioneDocumentiSvc.BusinessLayer.Test.csproj", "src/GestioneDocumentiSvc/GestioneDocumentiSvc.BusinessLayer.Test/"]
COPY ["src/GestioneDocumentiSvc/GestioneDocumentiSvc.DataAccessLayer/GestioneDocumentiSvc.DataAccessLayer.csproj", "src/GestioneDocumentiSvc/GestioneDocumentiSvc.DataAccessLayer/"]
#COPY ["src/GestioneDocumentiSvc/GestioneDocumentiSvc.IntegrationTest/GestioneDocumentiSvc.IntegrationTest.csproj", "src/GestioneDocumentiSvc/GestioneDocumentiSvc.IntegrationTest/"]
COPY ["src/GestioneDocumentiSvc/GestioneDocumentiSvc.Migrations/GestioneDocumentiSvc.Migrations.csproj", "src/GestioneDocumentiSvc/GestioneDocumentiSvc.Migrations/"]
COPY ["src/GestioneDocumentiSvc/GestioneDocumentiSvc.Shared/GestioneDocumentiSvc.Shared.csproj", "src/GestioneDocumentiSvc/GestioneDocumentiSvc.Shared/"]

RUN dotnet restore "src/GestioneDocumentiSvc/GestioneDocumentiSvc/GestioneDocumentiSvc.csproj"
COPY . .

WORKDIR "/src/src/GestioneDocumentiSvc/GestioneDocumentiSvc"
RUN dotnet build "GestioneDocumentiSvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GestioneDocumentiSvc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GestioneDocumentiSvc.dll"]