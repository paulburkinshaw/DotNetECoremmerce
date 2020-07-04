FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
# WORKDIR /src
COPY ./DotNetECoremmerce.ProductCatalogue.API.csproj ./ProductCatalogue/DotNetECoremmerce.ProductCatalogue.API/
WORKDIR "/ProductCatalogue/DotNetECoremmerce.ProductCatalogue.API"
RUN dotnet restore 

COPY . .

RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DotNetECoremmerce.ProductCatalogue.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotNetECoremmerce.ProductCatalogue.API.dll"]
