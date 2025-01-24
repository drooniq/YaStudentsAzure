# Stage 1: Build stage for dependencies (install sqlcmd)
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS build

# Stage 2: Build the .NET application
FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build_app

# Set working directory
WORKDIR /source

# Copy application files to the container
COPY . .

# Restore dependencies and publish the application
RUN dotnet restore
RUN dotnet publish -c Release -o /app

# Stage 3: Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS final

# Install necessary dependencies for localization support in the runtime container
RUN apk add --no-cache icu-libs icu-data-full bash

# Set environment variable to disable invariant mode and enable full globalization support
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# Set the working directory
WORKDIR /app

# Copy the application from the build stage
COPY --from=build_app /app .

# Set the entrypoint to run the application
ENTRYPOINT ["dotnet", "YaStudents.dll"]