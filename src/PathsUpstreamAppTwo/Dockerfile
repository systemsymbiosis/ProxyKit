FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/PathsUpstreamAppTwo/PathsUpstreamAppTwo.csproj", "src/PathsUpstreamAppTwo/"]
COPY ["src/ProxyKit/ProxyKit.csproj", "src/ProxyKit/"]
RUN dotnet restore "src/PathsUpstreamAppTwo/PathsUpstreamAppTwo.csproj"
COPY . .
WORKDIR "/src/src/PathsUpstreamAppTwo"
RUN dotnet build "PathsUpstreamAppTwo.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "PathsUpstreamAppTwo.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProxyKit.Recipe.PathsUpstreamAppTwo.dll"]