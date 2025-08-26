# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copy csproj and restore as distinct layers
COPY GroceryManagement.API/*.csproj ./GroceryManagement.API/
RUN dotnet restore GroceryManagement.API/GroceryManagement.API.csproj

# copy everything else and build
COPY . .
WORKDIR /src/GroceryManagement.API
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# expose the container port 8087

ENV ASPNETCORE_URLS=http://+:8087
EXPOSE 8088
ENTRYPOINT ["dotnet", "GroceryManagement.API.dll"]
