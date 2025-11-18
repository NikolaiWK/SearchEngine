FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY SearchApi/SearchApi.csproj ./SearchApi/
COPY Shared/*.csproj ./Shared/
COPY ConsoleSearch/*.csproj ./ConsoleSearch/
COPY indexer/*.csproj ./indexer/

RUN dotnet restore SearchApi/SearchApi.csproj

COPY . .
RUN dotnet publish SearchApi/SearchApi.csproj -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "SearchApi.dll"]
