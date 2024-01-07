FROM mcr.microsoft.com/dotnet/aspnet:2.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:2.1 AS build
WORKDIR /src
COPY ["CalcDistBeetwenTwoCordinates/CalcDistBeetwenTwoCordinates.csproj", "CalcDistBeetwenTwoCordinates/"]
RUN dotnet restore "CalcDistBeetwenTwoCordinates/CalcDistBeetwenTwoCordinates.csproj"
COPY . .
WORKDIR "/src/CalcDistBeetwenTwoCordinates"
RUN dotnet build "CalcDistBeetwenTwoCordinates.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CalcDistBeetwenTwoCordinates.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CalcDistBeetwenTwoCordinates.dll"]