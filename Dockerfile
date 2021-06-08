# Build
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
COPY . ./
RUN dotnet publish WebApi/WebApi.csproj -c Release -o /app/publish

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS final
RUN apt-get update && apt-get install -y --no-install-recommends libgdiplus
#ENV TZ=Europe/Moscow
#RUN apt-get update && apt-get install -y --no-install-recommends tzdata
#RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone
WORKDIR /app
COPY --from=build /app/publish .
COPY ./WebApi/Frontend ./Frontend
COPY ./wwwroot ./wwwrootCopied
ENTRYPOINT ["dotnet", "WebApi.dll"]