# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy all source files
# The .dockerignore file will prevent bin/obj from being copied
COPY . .

# Publish the application. This command also runs 'dotnet restore'
RUN dotnet publish "Basic Task Manager.csproj" -c Release -o /app/publish

# Stage 2: Create the final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy the published output from the 'build' stage
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Basic Task Manager.dll", "--urls", "http://0.0.0.0:${PORT}"]