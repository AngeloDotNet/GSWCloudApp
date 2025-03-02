FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/InvioEmailSvc/InvioEmailSvc/InvioEmailSvc.csproj", "src/InvioEmailSvc/InvioEmailSvc/"]
COPY ["src/InvioEmailSvc/InvioEmailSvc.BusinessLayer/InvioEmailSvc.BusinessLayer.csproj", "src/InvioEmailSvc/InvioEmailSvc.BusinessLayer/"]
#COPY ["src/InvioEmailSvc/InvioEmailSvc.BusinessLayer.Test/InvioEmailSvc.BusinessLayer.Test.csproj", "src/InvioEmailSvc/InvioEmailSvc.BusinessLayer.Test/"]
COPY ["src/InvioEmailSvc/InvioEmailSvc.DataAccessLayer/InvioEmailSvc.DataAccessLayer.csproj", "src/InvioEmailSvc/InvioEmailSvc.DataAccessLayer/"]
#COPY ["src/InvioEmailSvc/InvioEmailSvc.IntegrationTest/InvioEmailSvc.IntegrationTest.csproj", "src/InvioEmailSvc/InvioEmailSvc.IntegrationTest/"]
COPY ["src/InvioEmailSvc/InvioEmailSvc.Migrations/InvioEmailSvc.Migrations.csproj", "src/InvioEmailSvc/InvioEmailSvc.Migrations/"]
COPY ["src/InvioEmailSvc/InvioEmailSvc.Shared/InvioEmailSvc.Shared.csproj", "src/InvioEmailSvc/InvioEmailSvc.Shared/"]
COPY ["src/GSWCloudApp.Common/GSWCloudApp.Common.csproj", "src/GSWCloudApp.Common/"]

RUN dotnet restore "src/InvioEmailSvc/InvioEmailSvc/InvioEmailSvc.csproj"
COPY . .

WORKDIR "/src/src/InvioEmailSvc/InvioEmailSvc"
RUN dotnet build "InvioEmailSvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InvioEmailSvc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InvioEmailSvc.dll"]