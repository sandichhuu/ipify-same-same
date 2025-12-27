FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
RUN apk add --no-cache build-base zlib-dev musl-dev clang

WORKDIR /source

COPY --link ipify/. .
RUN dotnet publish ipify.csproj --runtime linux-musl-x64 -o /app

FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine
EXPOSE 8080
WORKDIR /app
COPY --link --from=build /app .
USER $APP_UID
ENTRYPOINT ["./ipify"]