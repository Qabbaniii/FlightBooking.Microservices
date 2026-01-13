FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["BookingProcessorService/BookingProcessorService.API/BookingProcessorService.API.csproj", "BookingProcessorService/BookingProcessorService.API/"]


RUN dotnet restore "BookingProcessorService/BookingProcessorService.API/BookingProcessorService.API.csproj"


COPY . .

WORKDIR "/src/BookingProcessorService/BookingProcessorService.API"


RUN dotnet build "BookingProcessorService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build


FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BookingProcessorService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookingProcessorService.API.dll"]
