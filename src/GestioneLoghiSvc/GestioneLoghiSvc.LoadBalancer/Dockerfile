FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/GestioneLoghiSvc/GestioneLoghiSvc.LoadBalancer/GestioneLoghiSvc.LoadBalancer.csproj", "src/GestioneLoghiSvc/GestioneLoghiSvc.LoadBalancer/"]
RUN dotnet restore "src/GestioneLoghiSvc/GestioneLoghiSvc.LoadBalancer/GestioneLoghiSvc.LoadBalancer.csproj"
COPY . .

WORKDIR "/src/src/GestioneLoghiSvc/GestioneLoghiSvc.LoadBalancer"
RUN dotnet build "GestioneLoghiSvc.LoadBalancer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GestioneLoghiSvc.LoadBalancer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GestioneLoghiSvc.LoadBalancer.dll"]