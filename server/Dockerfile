FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build-env
WORKDIR /server

# Copy csproj and restore as distinct layers
COPY WebAPI/*.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.1
COPY --from=build-env ./ .
WORKDIR /server/WebAPI/out
ENTRYPOINT ["dotnet", "WebAPI.dll"]
