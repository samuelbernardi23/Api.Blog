# Etapa 1: Imagem base para construir a aplicação
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Defina o diretório de trabalho dentro do contêiner
WORKDIR /src

# Copie o arquivo de solução (caso tenha) para o contêiner
COPY ["TopCon.Api.csproj", "TopCon.Api/"]

# Restaure as dependências
RUN dotnet restore "TopCon.Api.csproj"

# Copie o restante do código da aplicação
COPY . .

# Publique a aplicação para a pasta de saída /app
RUN dotnet publish "TopCon.Api.csproj" -c Release -o /app/publish

# Etapa 2: Imagem base para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:9.0

# Defina o diretório de trabalho no contêiner
WORKDIR /app

# Copie os arquivos publicados do contêiner anterior
COPY --from=build /app/publish .

# Exponha a porta que o aplicativo irá rodar
EXPOSE 80

# Defina o comando para rodar a aplicação
ENTRYPOINT ["dotnet", "TopCon.Api.dll"]
