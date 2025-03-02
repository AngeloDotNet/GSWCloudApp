FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/InvioEmailSvc/InvioEmailSvc.LoadBalancer/InvioEmailSvc.LoadBalancer.csproj", "src/InvioEmailSvc/InvioEmailSvc.LoadBalancer/"]
RUN dotnet restore "src/InvioEmailSvc/InvioEmailSvc.LoadBalancer/InvioEmailSvc.LoadBalancer.csproj"
COPY . .

WORKDIR "/src/src/InvioEmailSvc/InvioEmailSvc.LoadBalancer"
RUN dotnet build "InvioEmailSvc.LoadBalancer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InvioEmailSvc.LoadBalancer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InvioEmailSvc.LoadBalancer.dll"]