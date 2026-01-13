FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["FlightSearchService/FlightSearchService.API/FlightSearchService.API.csproj", "FlightSearchService/FlightSearchService.API/"]


RUN dotnet restore "FlightSearchService/FlightSearchService.API/FlightSearchService.API.csproj"


COPY . .

WORKDIR "/src/FlightSearchService/FlightSearchService.API"


RUN dotnet build "FlightSearchService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build


FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FlightSearchService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FlightSearchService.API.dll"]
