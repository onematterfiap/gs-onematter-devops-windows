# ----------------------------------------------------------------------
# STAGE 1: BUILD (Compilação)
# Usa a imagem SDK (mais completa) baseada em Alpine.
# ----------------------------------------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src

# Copia o arquivo de projeto e restaura dependências
COPY "OneMatter.csproj" .
RUN dotnet restore "OneMatter.csproj"

# Copia o código-fonte restante e faz o build final
COPY . .
RUN dotnet publish "OneMatter.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ----------------------------------------------------------------------
# STAGE 2: FINAL (Execução)
# Usa a imagem de Runtime mais leve (ASP.NET Core) baseada em Alpine.
# ----------------------------------------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
WORKDIR /app

# Usuário padrão 'app' (já configurado pela imagem base como não-root)
# REQUISITO: O aplicativo deve ser executado por um usuário que não possua privilégios
USER app 

# Copia o resultado da publicação
COPY --from=build /app/publish .

# O ASP.NET Core geralmente expõe a porta 8080 neste ambiente Docker
EXPOSE 8080

# Comando de execução da aplicação
CMD ["dotnet", "OneMatter.dll"]