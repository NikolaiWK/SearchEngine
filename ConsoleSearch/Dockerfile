FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ConsoleSearch/*.csproj ./ConsoleSearch/
COPY Shared/*.csproj ./Shared/

RUN dotnet restore ConsoleSearch/ConsoleSearch.csproj

COPY . .
RUN dotnet publish ConsoleSearch/ConsoleSearch.csproj -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/runtime:9.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "ConsoleSearch.dll"]
