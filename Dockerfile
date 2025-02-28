FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY TopCon.Api.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /out .

CMD ["dotnet", "TopCon.Api.dll"]
