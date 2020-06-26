FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["DotNetECoremmerce.ProductCatalogue.API/DotNetECoremmerce.ProductCatalogue.API.csproj", "DotNetECoremmerce.ProductCatalogue.API/"]
RUN dotnet restore "DotNetECoremmerce.ProductCatalogue.API/DotNetECoremmerce.ProductCatalogue.API.csproj"
COPY . .
WORKDIR "/src/DotNetECoremmerce.ProductCatalogue.API"
RUN dotnet build "DotNetECoremmerce.ProductCatalogue.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DotNetECoremmerce.ProductCatalogue.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotNetECoremmerce.ProductCatalogue.API.dll"]
