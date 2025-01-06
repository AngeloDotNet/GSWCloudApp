FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/GestioneDocumentiSvc/GestioneDocumentiSvc.LoadBalancer/GestioneDocumentiSvc.LoadBalancer.csproj", "src/GestioneDocumentiSvc/GestioneDocumentiSvc.LoadBalancer/"]
RUN dotnet restore "src/GestioneDocumentiSvc/GestioneDocumentiSvc.LoadBalancer/GestioneDocumentiSvc.LoadBalancer.csproj"
COPY . .

WORKDIR "/src/src/GestioneDocumentiSvc/GestioneDocumentiSvc.LoadBalancer"
RUN dotnet build "GestioneDocumentiSvc.LoadBalancer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GestioneDocumentiSvc.LoadBalancer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GestioneDocumentiSvc.LoadBalancer.dll"]