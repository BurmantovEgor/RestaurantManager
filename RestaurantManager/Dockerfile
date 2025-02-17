FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Копируем файлы проекта
COPY ApiGateway.csproj ./
RUN dotnet restore

# Копируем весь код и собираем проект
COPY . .  
RUN dotnet publish -c Release -o /app/publish

# Используем минимальный runtime-образ для запуска
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Запускаем приложение
ENTRYPOINT ["dotnet", "ApiGateway.dll"]
