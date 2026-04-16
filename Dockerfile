FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/SmartPot.Api/SmartPot.Api.csproj", "src/SmartPot.Api/"]
RUN dotnet restore "src/SmartPot.Api/SmartPot.Api.csproj"
COPY . .
WORKDIR "/src/src/SmartPot.Api"
RUN dotnet build "SmartPot.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SmartPot.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SmartPot.Api.dll"]
