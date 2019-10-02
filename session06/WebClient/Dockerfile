FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["WebClient.csproj", ""]
RUN dotnet restore "./WebClient.csproj"
COPY . .
WORKDIR /src
RUN dotnet build "WebClient.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "WebClient.csproj" -c Release -o /app

FROM node:carbon AS node
COPY . .
#RUN npm set progress=false && npm config set depth 0
RUN npm install 
#RUN npm install --only=production 
RUN npm run build

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
COPY --from=node wwwroot ./wwwroot
EXPOSE 80
EXPOSE 443
EXPOSE 5000:5001

ENTRYPOINT ["dotnet", "WebClient.dll"]