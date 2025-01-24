# Stage 1: Build stage for dependencies (install sqlcmd)
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS build

# Install necessary dependencies for handling .bz2 files, others, and localization support
RUN apk add --no-cache curl tar bzip2 unixodbc-dev icu-libs icu-data-full bash

# Download and install go-sqlcmd from GitHub release
RUN curl -LO https://github.com/microsoft/go-sqlcmd/releases/download/v1.8.2/sqlcmd-linux-arm64.tar.bz2 \
    && tar -xvjf sqlcmd-linux-arm64.tar.bz2 \
    && mv sqlcmd /usr/local/bin/ \
    && chmod +x /usr/local/bin/sqlcmd \
    && rm -f sqlcmd-linux-arm64.tar.bz2

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

# Copy sqlcmd from the build stage
COPY --from=build /usr/local/bin/sqlcmd /usr/local/bin/sqlcmd

# Set the entrypoint to run the application
ENTRYPOINT ["dotnet", "YaStudents.dll"]