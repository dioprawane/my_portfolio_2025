# Étape 1 : Utilisez une image qui contient le SDK .NET pour la publication
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Copiez le fichier .csproj et restaurez les dépendances
COPY *.csproj .
RUN dotnet restore

# Copiez le reste du code source
COPY . .

# Publiez l'application en mode Release
RUN dotnet publish -c Release -o /app/publish

# Étape 2 : Utilisez une image plus légère pour l'exécution
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish/wwwroot .