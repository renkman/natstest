FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ENV COMPlus_EnableDiagnostics=0

WORKDIR /src

COPY ../src/dotnet/NatsTest ./

RUN dotnet restore

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim AS app
WORKDIR /app
COPY --from=build /src/out .
EXPOSE 8080
ENTRYPOINT ["dotnet", "NatsTest.dll"]
