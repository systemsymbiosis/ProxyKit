FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/PathsProxy/PathsProxy.csproj", "src/PathsProxy/"]
COPY ["src/ProxyKit/ProxyKit.csproj", "src/ProxyKit/"]
RUN dotnet restore "src/PathsProxy/PathsProxy.csproj"
COPY . .
WORKDIR "/src/src/PathsProxy"
RUN dotnet build "PathsProxy.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "PathsProxy.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProxyKit.Recipe.PathsProxy.dll"]