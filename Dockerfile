#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Plantr.Gateway/Plantr.Gateway.csproj", "Plantr.Gateway/"]
RUN dotnet restore "Plantr.Gateway/Plantr.Gateway.csproj"
COPY . .
WORKDIR "/src/Plantr.Gateway"
RUN dotnet build "Plantr.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Plantr.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Plantr.Gateway.dll"]