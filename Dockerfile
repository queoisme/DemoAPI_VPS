FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY DemoAPI_VPS/*.csproj ./DemoAPI/
RUN dotnet restore ./DemoAPI_VPS/DemoAPI_VPS.csproj

COPY . .
RUN dotnet publish ./DemoAPI_VPS/DemoAPI_VPS.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "DemoAPI_VPS.dll"]