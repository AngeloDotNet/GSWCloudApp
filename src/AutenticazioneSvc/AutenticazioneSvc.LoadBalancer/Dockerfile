FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/AutenticazioneSvc/AutenticazioneSvc.LoadBalancer/AutenticazioneSvc.LoadBalancer.csproj", "src/AutenticazioneSvc/AutenticazioneSvc.LoadBalancer/"]
RUN dotnet restore "src/AutenticazioneSvc/AutenticazioneSvc.LoadBalancer/AutenticazioneSvc.LoadBalancer.csproj"
COPY . .

WORKDIR "/src/src/AutenticazioneSvc/AutenticazioneSvc.LoadBalancer"
RUN dotnet build "AutenticazioneSvc.LoadBalancer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AutenticazioneSvc.LoadBalancer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AutenticazioneSvc.LoadBalancer.dll"]