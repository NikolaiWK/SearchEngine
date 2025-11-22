FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY SearchApi/*.csproj ./SearchApi/
COPY Shared/*.csproj ./Shared/
COPY ConsoleSearch/*.csproj ./ConsoleSearch/
COPY indexer/*.csproj ./indexer/

RUN dotnet restore SearchApi/SearchApi.csproj

COPY SearchApi/. ./SearchApi/
COPY Shared/. ./Shared/
COPY ConsoleSearch/. ./ConsoleSearch/
COPY indexer/. ./indexer/

RUN dotnet publish SearchApi/SearchApi.csproj -c Release -o /app/out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# create the folder that the SQLite DB will be mounted into
RUN mkdir -p /app/data

COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "SearchApi.dll"]
