# Используем .NET Framework SDK для сборки
FROM mcr.microsoft.com/dotnet/framework/sdk:4.8-windowsservercore-ltsc2019 AS build
WORKDIR /src

# Копируем файл проекта и восстанавливаем зависимости
COPY ["Scanner.csproj", "./"]
RUN nuget restore "Scanner.csproj"

# Копируем остальные файлы проекта
COPY . .

# Сборка проекта
RUN msbuild "Scanner.csproj" /p:Configuration=Release /p:OutputPath=/app/build

# Используем минимальный образ для запуска
FROM mcr.microsoft.com/dotnet/framework/runtime:4.8-windowsservercore-ltsc2019 AS runtime
WORKDIR /app

# Копируем опубликованные файлы из стадии сборки
COPY --from=build /app/build .

# Указываем точку входа
ENTRYPOINT ["Scanner.exe"]
