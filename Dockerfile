FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY ["Docker-Example.csproj", "./"]
RUN dotnet restore "Docker-Example.csproj"

COPY . . 
RUN dotnet publish "Docker-Example.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app
COPY --from=build /app/publish . 
EXPOSE 8080
ENTRYPOINT ["dotnet", "Docker-Example.dll"]