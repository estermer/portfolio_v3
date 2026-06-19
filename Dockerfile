# Stage 1: Restore + publish
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj first for layer caching
COPY EricstermerCom.csproj ./
RUN dotnet restore EricstermerCom.csproj

# Copy source and publish
COPY . ./
RUN dotnet publish EricstermerCom.csproj \
    -c Release \
    -o /app/publish \
    --no-restore \
    /p:UseAppHost=false

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Install ICU for globalization support
RUN apt-get update && apt-get install -y --no-install-recommends \
    libicu74 \
    && rm -rf /var/lib/apt/lists/*

# Create non-root user
RUN groupadd --system --gid 1001 ericstermer \
    && useradd --system --uid 1001 --gid ericstermer \
    --home /app --shell /sbin/nologin ericstermer

# Copy published app
COPY --from=build /app/publish ./

# Create data directory for SQLite (volume mount target)
RUN mkdir -p /app/data && chown -R ericstermer:ericstermer /app

USER ericstermer

ENV ASPNETCORE_URLS=http://+:8080 \
    ASPNETCORE_ENVIRONMENT=Production \
    ConnectionStrings__Default="Data Source=/app/data/ericstermer.db" \
    Admin__Email=admin@ericstermer.com \
    Admin__Password=CHANGE_ME_IN_PRODUCTION

EXPOSE 8080

HEALTHCHECK --interval=30s --timeout=5s --start-period=10s --retries=3 \
    CMD wget --quiet --tries=1 --spider http://localhost:8080/ || exit 1

ENTRYPOINT ["dotnet", "EricstermerCom.dll"]
