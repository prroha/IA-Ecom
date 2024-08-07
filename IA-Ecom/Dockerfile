# Use the ASP.NET Core runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

# Set the user ID for the container for security
USER $APP_UID

# Set the working directory in the container
WORKDIR /app

# Expose ports 8080 and 8081
EXPOSE 8080
EXPOSE 8081

# Use the .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Argument for build configuration, default is Release
ARG BUILD_CONFIGURATION=Release

# Set the working directory for the build process
WORKDIR /src

# Copy the project file to the container
COPY ["IA-Ecom/IA-Ecom.csproj", "IA-Ecom/"]

# Restore the project dependencies
RUN dotnet restore "IA-Ecom/IA-Ecom.csproj"

# Copy all files to the container
COPY . .

# Set the working directory to the project folder
WORKDIR "/src/IA-Ecom"

# Build the project
RUN dotnet build "IA-Ecom.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the project to the /app/publish directory
FROM build AS publish

# Argument for build configuration, default is Release
ARG BUILD_CONFIGURATION=Release

# Publish the project
RUN dotnet publish "IA-Ecom.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Use the base runtime image for the final stage
FROM base AS final

# Set the working directory in the container
WORKDIR /app

# Copy the published output from the publish stage
COPY --from=publish /app/publish .

# Set the entry point to run the application
ENTRYPOINT ["dotnet", "IA-Ecom.dll"]
