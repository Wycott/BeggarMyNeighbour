FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["BeggarMyNeighbour/BeggarMyNeighbour.csproj", "BeggarMyNeighbour/"]
COPY ["BeggarMyNeighbourLibrary/BeggarMyNeighbourLibrary.csproj", "BeggarMyNeighbourLibrary/"]
RUN dotnet restore "BeggarMyNeighbour/BeggarMyNeighbour.csproj"
COPY . .
WORKDIR "/src/BeggarMyNeighbour"
RUN dotnet build "BeggarMyNeighbour.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BeggarMyNeighbour.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/runtime:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BeggarMyNeighbour.dll"]
