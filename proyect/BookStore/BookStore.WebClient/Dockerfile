FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["BookStore.WebClient/BookStore.WebClient.csproj", "BookStore.WebClient/"]
COPY ["BookStore.Entities/BookStore.Entities.csproj", "BookStore.Entities/"]
RUN dotnet restore "BookStore.WebClient/BookStore.WebClient.csproj"
COPY . .
WORKDIR "/src/BookStore.WebClient"
RUN dotnet build "BookStore.WebClient.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookStore.WebClient.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookStore.WebClient.dll"]