# Sử dụng .NET 7 SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy project và restore dependencies
COPY *.csproj .
RUN dotnet restore

# Build ứng dụng
COPY . .
RUN dotnet publish -c Release -o /publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /publish .

# Chạy ứng dụng
ENTRYPOINT ["dotnet", "ShoeStore.dll"]
