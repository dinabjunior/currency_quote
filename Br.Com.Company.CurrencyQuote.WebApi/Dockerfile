FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Br.Com.Company.CurrencyQuote.WebApi/Br.Com.Company.CurrencyQuote.WebApi.csproj", "Br.Com.Company.CurrencyQuote.WebApi/"]
RUN dotnet restore "Br.Com.Company.CurrencyQuote.WebApi/Br.Com.Company.CurrencyQuote.WebApi.csproj"
COPY . .
WORKDIR "/src/Br.Com.Company.CurrencyQuote.WebApi"
RUN dotnet build "Br.Com.Company.CurrencyQuote.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Br.Com.Company.CurrencyQuote.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Br.Com.Company.CurrencyQuote.WebApi.dll"]