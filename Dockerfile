# syntax=docker/dockerfile:1.0.0-experimental
FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env

ARG PAT

WORKDIR /src
COPY ./src ./

WORKDIR /tests
COPY ./tests ./

RUN dotnet restore

RUN apt-get update && apt-get install lsof

ENTRYPOINT ["dotnet", "test"]