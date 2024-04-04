FROM mcr.microsoft.com/dotnet/sdk:8.0 as copyStage
WORKDIR /src
COPY src/ ./
RUN find . -type f -not -name '*.csproj' -delete

FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
ENV NUGET_XMLDOC_MODE=none
WORKDIR /src

COPY --from=copyStage /src .
WORKDIR /src/SaAn/SaAn.Web.Blazor
RUN dotnet restore

WORKDIR /src
COPY src/ ./
RUN find . -type f -name 'appsettings.*.json' -delete

WORKDIR /src/SaAn/SaAn.Web.Blazor
RUN dotnet build -c Release -o /app/build --no-restore
RUN dotnet publish -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
LABEL maintainer=antonio.canzanella@precipoint.de

WORKDIR /app
COPY --from=build /app/publish ./
COPY --from=build /app/build/PreciPoint.Ims.*.xml ./

ENTRYPOINT ["dotnet", "SaAn.Web.Blazor.dll"]
