# Étape 1 : Utilisez une image qui contient le SDK .NET pour la publication
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Copiez le fichier .csproj et restaurez les dépendances
COPY BlazorPortfolio.csproj .
RUN dotnet restore

# Copiez le reste du code source
COPY . .

# Publiez l'application en mode Release
RUN dotnet publish -c Release -o /app/publish

# Étape 2 : Utilisez une image Nginx pour servir les fichiers statiques
FROM nginx:alpine AS final

# Copiez les fichiers statiques de la phase de compilation vers le répertoire de Nginx
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html

# Exposez le port 80 pour que Nginx puisse écouter
EXPOSE 80