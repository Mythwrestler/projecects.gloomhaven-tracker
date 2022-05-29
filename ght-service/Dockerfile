FROM dotnet/sdk:6.0 AS builder

WORKDIR /app

# Copy in project files
COPY nuget.config ./
COPY *.csproj ./

# Restore Nuget Packages
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

FROM dotnet/aspnet:6.0

WORKDIR /app

COPY --from=builder /app/out .

ENTRYPOINT ["dotnet", "ght-service.dll"]