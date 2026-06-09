FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY DemoAPI/*.csproj ./DemoAPI/
RUN dotnet restore ./DemoAPI/DemoAPI.csproj

COPY . .
RUN dotnet publish ./DemoAPI/DemoAPI.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "DemoApi.dll"]