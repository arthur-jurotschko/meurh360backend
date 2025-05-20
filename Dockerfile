# Usar imagem oficial do .NET SDK para build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copiar arquivos do projeto e restaurar dependências
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app/out

# Usar imagem runtime do .NET para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Copiar arquivos do estágio de build
COPY --from=build /app/out .

# Definir entrada do container
ENTRYPOINT ["dotnet", "meurh360backend.dll"]
