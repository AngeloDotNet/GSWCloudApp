FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/GatewaySvc/GatewaySvc/GatewaySvc.csproj", "src/GatewaySvc/GatewaySvc/"]

# Copy the Ocelot configuration files
COPY ["src/GatewaySvc/GatewaySvc/ConfigOcelot/ocelot.configurazionesmtp.json", "src/GatewaySvc/GatewaySvc/ConfigOcelot/"]
COPY ["src/GatewaySvc/GatewaySvc/ConfigOcelot/ocelot.configurazionisender.json", "src/GatewaySvc/GatewaySvc/ConfigOcelot/"]
COPY ["src/GatewaySvc/GatewaySvc/ConfigOcelot/ocelot.gestionedocumenti.json", "src/GatewaySvc/GatewaySvc/ConfigOcelot/"]
COPY ["src/GatewaySvc/GatewaySvc/ConfigOcelot/ocelot.gestioneloghi.json", "src/GatewaySvc/GatewaySvc/ConfigOcelot/"]

# Servizio non raggiungibile dall'esterno
#COPY ["src/GatewaySvc/GatewaySvc/ConfigOcelot/ocelot.invioemail.json", "src/GatewaySvc/GatewaySvc/ConfigOcelot/"]

COPY ["src/GSWCloudApp.Common/GSWCloudApp.Common.csproj", "src/GSWCloudApp.Common/"]

RUN dotnet restore "src/GatewaySvc/GatewaySvc/GatewaySvc.csproj"
COPY . .

WORKDIR "/src/src/GatewaySvc/GatewaySvc"
RUN dotnet build "GatewaySvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GatewaySvc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GatewaySvc.dll"]