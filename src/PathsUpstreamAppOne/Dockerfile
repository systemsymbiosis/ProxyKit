FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/PathsUpstreamAppOne/PathsUpstreamAppOne.csproj", "src/PathsUpstreamAppOne/"]
COPY ["src/ProxyKit/ProxyKit.csproj", "src/ProxyKit/"]
RUN dotnet restore "src/PathsUpstreamAppOne/PathsUpstreamAppOne.csproj"
COPY . .
WORKDIR "/src/src/PathsUpstreamAppOne"
RUN dotnet build "PathsUpstreamAppOne.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "PathsUpstreamAppOne.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProxyKit.PathsUpstreamAppOne.dll"]