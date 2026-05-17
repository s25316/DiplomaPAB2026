# ==========================================
# Etap 1: Środowisko uruchomieniowe (Runtime)
# ==========================================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# ==========================================
# Etap 2: Kompilacja i budowanie (Build)
# ==========================================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Kopiowanie plików definicji pakietów (jeśli używasz Central Package Management)
COPY ["Directory.Packages.props", "./"]

# Kopiowanie pliku projektu z zachowaniem struktury folderów
COPY ["GUS.REGON/GUS.REGON.API/GUS.REGON.API.csproj", "GUS.REGON/GUS.REGON.API/"]

# Przywracanie zależności (Nuget restore)
RUN dotnet restore "GUS.REGON/GUS.REGON.API/GUS.REGON.API.csproj"

# Kopiowanie całej reszty kodu źródłowego
COPY . .

# Zmiana katalogu na folder projektu i budowanie aplikacji
WORKDIR "/src/GUS.REGON/GUS.REGON.API"
RUN dotnet build "GUS.REGON.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# ==========================================
# Etap 3: Publikacja aplikacji (Publish)
# ==========================================
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
# Publikujemy projekt z bieżącego WORKDIR (czyli kropka ".")
RUN dotnet publish . -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# ==========================================
# Etap 4: Obraz końcowy (Final production)
# ==========================================
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GUS.REGON.API.dll"]