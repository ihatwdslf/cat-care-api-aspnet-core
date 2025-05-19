# Базовий образ для запуску
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

# Етап побудови
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копіюємо проект та відновлюємо залежності
COPY *.csproj ./ 
RUN dotnet restore

# Копіюємо решту коду і збираємо
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Фінальний образ
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Вкажи правильну назву, якщо вона інша
ENTRYPOINT ["dotnet", "CatCareApi.dll"]
