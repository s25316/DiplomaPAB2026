FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Wskazujemy dokładną ścieżkę do pliku csproj względem głównego folderu rozwiązania
COPY ["Frontend/Frontend/Frontend.csproj", "Frontend/Frontend/"]
RUN dotnet restore "Frontend/Frontend/Frontend.csproj"

# Kopiujemy całą resztę kodu
COPY . .
WORKDIR "/src/Frontend/Frontend"
RUN dotnet build "Frontend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Frontend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Frontend.dll"]