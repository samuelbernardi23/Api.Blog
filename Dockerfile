# Etapa 1: Imagem base para construir a aplica��o
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Defina o diret�rio de trabalho dentro do cont�iner
WORKDIR /src

# Copie o arquivo de solu��o (caso tenha) para o cont�iner
COPY ["TopCon.Api.csproj", "TopCon.Api/"]

# Restaure as depend�ncias
RUN dotnet restore "TopCon.Api.csproj"

# Copie o restante do c�digo da aplica��o
COPY . .

# Publique a aplica��o para a pasta de sa�da /app
RUN dotnet publish "TopCon.Api.csproj" -c Release -o /app/publish

# Etapa 2: Imagem base para rodar a aplica��o
FROM mcr.microsoft.com/dotnet/aspnet:9.0

# Defina o diret�rio de trabalho no cont�iner
WORKDIR /app

# Copie os arquivos publicados do cont�iner anterior
COPY --from=build /app/publish .

# Exponha a porta que o aplicativo ir� rodar
EXPOSE 80

# Defina o comando para rodar a aplica��o
ENTRYPOINT ["dotnet", "TopCon.Api.dll"]
