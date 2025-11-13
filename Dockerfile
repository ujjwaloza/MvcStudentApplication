# Use official .NET SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copy csproj and restore
COPY *.sln .
COPY MvcStudentApp/*.csproj ./MvcStudentApp/
RUN dotnet restore MvcStudentApp/MvcStudentApp.csproj

# Copy everything else and build
COPY . .
RUN dotnet publish MvcStudentApp/MvcStudentApp.csproj -c Release -o /app

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "MvcStudentApp.dll"]
