ARG REGISTRY=mcr.microsoft.com/
FROM ${REGISTRY}dotnet/sdk:6.0 AS builder

WORKDIR /app

# Copy in project files
COPY Database/ght-database.csproj ./Database/ght-database.csproj
COPY Service/ght-service.csproj ./Service/ght-service.csproj
COPY ght-utility.sln ./

# Restore Nuget Packages
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish ./Service/ght-service.csproj -c Release -o out

ARG REGISTRY=mcr.microsoft.com/
FROM ${REGISTRY}dotnet/aspnet:6.0

WORKDIR /app

COPY --from=builder /app/out .

ENTRYPOINT ["dotnet", "ght-service.dll"]