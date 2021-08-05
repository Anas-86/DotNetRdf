FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TriplesProject/TriplesProject.csproj", "TriplesProject/"]
RUN dotnet restore "TriplesProject/TriplesProject.csproj"
COPY . .
WORKDIR "/src/TriplesProject"
RUN dotnet build "TriplesProject.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TriplesProject.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TriplesProject.dll"]
