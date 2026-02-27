# Etapa de build con SDK 10
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiamos solo el csproj primero
COPY InvoiceManager.csproj ./
RUN dotnet restore

# Ahora copiamos todo el código
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Etapa final: runtime 10
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "InvoiceManager.dll"]