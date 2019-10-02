FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["./BookStore.WebApi", "BookStore.WebApi"]
COPY ["./BookStore.Entities", "BookStore.Entities"]
RUN dotnet restore "./BookStore.WebApi/BookStore.WebApi.csproj"
COPY ["./BookStore.WebApi", ""]
WORKDIR "BookStore.WebApi"

RUN dotnet build "BookStore.WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "BookStore.WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

# BP-01 Tip: Use EnvironmentVariables and Secrets
ENV BookStoreDatabase="Data Source=localhost,1433;User ID=sa;Password=Password.123;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
ENTRYPOINT ["dotnet", "BookStore.WebApi.dll"]

# cd material && docker build --rm -f "BookStore.WebApi/Dockerfile" -t BookStoreWebApi:latest .