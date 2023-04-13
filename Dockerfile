# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copy the code into the container
COPY . ./

# Restore NuGet packages
RUN dotnet restore ./3rdPartyAPI/3rdPartyAPI.sln

# Build the application
RUN dotnet build ./3rdPartyAPI/3rdPartyAPI.sln --configuration Release --no-restore

# Test the application
RUN dotnet test ./3rdPartyAPITest/3rdPartyAPITest.csproj --configuration Release --no-build

# Publish the application
RUN dotnet publish ./3rdPartyAPI/3rdPartyAPI.csproj --configuration Release --no-build --output /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /app/publish .

# Set the environment variable for the HTTP and HTTPS bindings
# ENV ASPNETCORE_URLS=https://+:443;http://+:80

# Expose ports 80 and 443
EXPOSE 80 443

# Set the entry point to run the application
ENTRYPOINT ["dotnet", "3rdPartyAPI.dll"]
