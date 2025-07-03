# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:9.0.301 AS build
WORKDIR /src

# Copia a solução e todos os arquivos do projeto
COPY . .

# Restaura as dependências
RUN dotnet restore ManaFood.sln

# Publica a aplicação
WORKDIR /src/Presentation/ManaFood.WebAPI
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "ManaFood.WebAPI.dll"]