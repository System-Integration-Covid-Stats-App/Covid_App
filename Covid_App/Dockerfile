FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Covid_App/Covid_App.csproj", "Covid_App/"]
RUN dotnet restore "Covid_App/Covid_App.csproj"
COPY . .
WORKDIR "/src/Covid_App"
RUN dotnet build "Covid_App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Covid_App.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY "Covid_App/Assets/data.xml" /app/Assets
ENTRYPOINT ["dotnet", "Covid_App.dll"]
