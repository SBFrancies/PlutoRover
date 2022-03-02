#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["PlutoRover.Api/PlutoRover.Api.csproj", "PlutoRover.Api/"]
RUN dotnet restore "PlutoRover.Api/PlutoRover.Api.csproj"
COPY . .
WORKDIR "/src/PlutoRover.Api"
RUN dotnet build "PlutoRover.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PlutoRover.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PlutoRover.Api.dll"]