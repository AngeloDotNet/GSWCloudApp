FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/ConfigurazioniSvc/ConfigurazioniSvc.LoadBalancer/ConfigurazioniSvc.LoadBalancer.csproj", "src/ConfigurazioniSvc/ConfigurazioniSvc.LoadBalancer/"]
RUN dotnet restore "src/ConfigurazioniSvc/ConfigurazioniSvc.LoadBalancer/ConfigurazioniSvc.LoadBalancer.csproj"
COPY . .

WORKDIR "/src/src/ConfigurazioniSvc/ConfigurazioniSvc.LoadBalancer"
RUN dotnet build "ConfigurazioniSvc.LoadBalancer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConfigurazioniSvc.LoadBalancer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConfigurazioniSvc.LoadBalancer.dll"]