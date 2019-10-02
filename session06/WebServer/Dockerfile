FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["./WebServer", "WebServer"]
COPY ["./AdventureWorks", "AdventureWorks"]
RUN dotnet restore "./WebServer/WebServer.csproj"
COPY ["./WebServer", ""]
WORKDIR "WebServer"

RUN dotnet build "WebServer.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "WebServer.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

# BP-01 Tip: Use EnvironmentVariables and Secrets
ENV AdventureWorksDatabase="Data Source=localhost,1433;User ID=sa;Password=Password.123;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
ENTRYPOINT ["dotnet", "WebServer.dll"]

# cd material && docker build --rm -f "WebServer/Dockerfile" -t webserver:latest .