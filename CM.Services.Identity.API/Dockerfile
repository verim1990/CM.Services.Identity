FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 19267
EXPOSE 44300

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY CM.Services/CM.Services.Identity/CM.Services.Identity.API/CM.Services.Identity.API.csproj CM.Services/CM.Services.Identity/CM.Services.Identity.API/
COPY CM.Shared/CM.Shared.Web/CM.Shared.Web.csproj CM.Shared/CM.Shared.Web/
COPY CM.Services/CM.Services.Identity/CM.Services.Identity.Concrete/CM.Services.Identity.Concrete.csproj CM.Services/CM.Services.Identity/CM.Services.Identity.Concrete/
COPY CM.Services/CM.Services.Identity/CM.Services.Identity.Infrastracture/CM.Services.Identity.Infrastracture.csproj CM.Services/CM.Services.Identity/CM.Services.Identity.Infrastracture/
RUN dotnet restore CM.Services/CM.Services.Identity/CM.Services.Identity.API/CM.Services.Identity.API.csproj
COPY . .
WORKDIR /src/CM.Services/CM.Services.Identity/CM.Services.Identity.API
RUN dotnet build CM.Services.Identity.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish CM.Services.Identity.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CM.Services.Identity.API.dll"]
