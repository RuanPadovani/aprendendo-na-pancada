FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["IdentityService/Api/IdentityService.Api.csproj", "IdentityService/Api/"]
COPY ["IdentityService/Application/IdentityService.Application.csproj", "IdentityService/Application/"]
COPY ["IdentityService/Domain/IdentityService.Domain.csproj", "IdentityService/Domain/"]
COPY ["IdentityService/Infrastructure/IdentityService.Infrastructure.csproj", "IdentityService/Infrastructure/"]

RUN dotnet restore "IdentityService/Api/IdentityService.Api.csproj"


COPY . .

RUN dotnet publish "IdentityService/Api/IdentityService.Api.csproj" \
    -c Release \
    -o /app/publish \
    --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT [ "dotnet", "IdentityService.Api.dll" ]