﻿FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DAPRWebClient.csproj", "DAPRWebClient/"]
RUN dotnet restore "DAPRWebClient/DAPRWebClient.csproj"
COPY . .
WORKDIR "/src/DAPRWebClient"
RUN dotnet build "DAPRWebClient.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DAPRWebClient.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DAPRWebClient.dll"]
