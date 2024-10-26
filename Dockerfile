FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
COPY src/ReadList.Api/ReadList.Api.csproj ReadList.Api/
RUN dotnet restore ReadList.Api/ReadList.Api.csproj

COPY ./src .
RUN dotnet publish ReadList.Api/ReadList.Api.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

RUN apt-get update && apt-get install -y coreutils
CMD ["sh", "-c", "dotnet ReadList.Api.dll"]

EXPOSE 80