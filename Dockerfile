# Use official ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["MvcStudentApp.csproj", "./"]
RUN dotnet restore "MvcStudentApp.csproj"

# Copy everything and build
COPY . .
RUN dotnet publish "MvcStudentApp.csproj" -c Release -o /app/publish

# Final image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MvcStudentApp.dll"]
