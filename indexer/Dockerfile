FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY indexer/*.csproj ./indexer/
COPY Shared/*.csproj ./Shared/
COPY ConsoleSearch/*.csproj ./ConsoleSearch/
COPY SearchApi/*.csproj ./SearchApi/

RUN dotnet restore indexer/indexer.csproj

COPY . .
RUN dotnet publish indexer/indexer.csproj -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/runtime:9.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "indexer.dll"]
