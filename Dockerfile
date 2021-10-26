FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
EXPOSE 443

COPY NuGet.Config .
COPY Unique.Profile.API/*.csproj ./Unique.Profile.API/

WORKDIR /app
COPY Unique.Profile.API/. ./Unique.Profle.API/
COPY . .

WORKDIR /app/Unique.Profile.API
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
ENV COMPlus_EnableDiagnostics=0
ENV ASPNETCORE_URLS="https://+"
ARG BUILD_DATETIME
ENV BUILD_VERSION=$BUILD_DATETIME
WORKDIR /app
COPY --from=build /app/Unique.Profile.API/out ./

# Bellow you should load your certificate in the PFX format
COPY mydomain.com.pfx ./
ENTRYPOINT ["dotnet", "Unique.Profile.API.dll"]