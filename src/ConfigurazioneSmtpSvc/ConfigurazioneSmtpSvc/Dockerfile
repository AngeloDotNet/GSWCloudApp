FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/ConfigurazioneSmtpSvc/ConfigurazioneSmtpSvc/ConfigurazioneSmtpSvc.csproj", "src/ConfigurazioneSmtpSvc/ConfigurazioneSmtpSvc/"]
COPY ["src/ConfigurazioneSmtpSvc/ConfigurazioneSmtpSvc.BusinessLayer/ConfigurazioneSmtpSvc.BusinessLayer.csproj", "src/ConfigurazioneSmtpSvc/ConfigurazioneSmtpSvc.BusinessLayer/"]
#COPY ["src/ConfigurazioneSmtpSvc/ConfigurazioneSmtpSvc.BusinessLayer.Test/ConfigurazioneSmtpSvc.BusinessLayer.Test.csproj", "src/ConfigurazioneSmtpSvc/ConfigurazioneSmtpSvc.BusinessLayer.Test/"]
COPY ["src/ConfigurazioneSmtpSvc/ConfigurazioneSmtpSvc.DataAccessLayer/ConfigurazioneSmtpSvc.DataAccessLayer.csproj", "src/ConfigurazioneSmtpSvc/ConfigurazioneSmtpSvc.DataAccessLayer/"]
#COPY ["src/ConfigurazioneSmtpSvc/ConfigurazioneSmtpSvc.IntegrationTest/ConfigurazioneSmtpSvc.IntegrationTest.csproj", "src/ConfigurazioneSmtpSvc/ConfigurazioneSmtpSvc.IntegrationTest/"]
COPY ["src/ConfigurazioneSmtpSvc/ConfigurazioneSmtpSvc.Migrations/ConfigurazioneSmtpSvc.Migrations.csproj", "src/ConfigurazioneSmtpSvc/ConfigurazioneSmtpSvc.Migrations/"]
COPY ["src/ConfigurazioneSmtpSvc/ConfigurazioneSmtpSvc.Shared/ConfigurazioneSmtpSvc.Shared.csproj", "src/ConfigurazioneSmtpSvc/ConfigurazioneSmtpSvc.Shared/"]
COPY ["src/GSWCloudApp.Common/GSWCloudApp.Common.csproj", "src/GSWCloudApp.Common/"]

RUN dotnet restore "src/ConfigurazioneSmtpSvc/ConfigurazioneSmtpSvc/ConfigurazioneSmtpSvc.csproj"
COPY . .

WORKDIR "/src/src/ConfigurazioneSmtpSvc/ConfigurazioneSmtpSvc"
RUN dotnet build "ConfigurazioneSmtpSvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConfigurazioneSmtpSvc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConfigurazioneSmtpSvc.dll"]