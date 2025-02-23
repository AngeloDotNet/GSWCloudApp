FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/ConfigurazioneSmtpSvc/ConfigurazioneSmtpSvc.LoadBalancer/ConfigurazioneSmtpSvc.LoadBalancer.csproj", "src/ConfigurazioneSmtpSvc/ConfigurazioneSmtpSvc.LoadBalancer/"]
RUN dotnet restore "src/ConfigurazioneSmtpSvc/ConfigurazioneSmtpSvc.LoadBalancer/ConfigurazioneSmtpSvc.LoadBalancer.csproj"
COPY . .

WORKDIR "/src/src/ConfigurazioneSmtpSvc/ConfigurazioneSmtpSvc.LoadBalancer"
RUN dotnet build "ConfigurazioneSmtpSvc.LoadBalancer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConfigurazioneSmtpSvc.LoadBalancer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConfigurazioneSmtpSvc.LoadBalancer.dll"]