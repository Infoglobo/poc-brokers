#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app

EXPOSE 80
EXPOSE 443
EXPOSE 9000

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["consumer-with-worker-rabbit-mq.csproj", "./"]
RUN dotnet restore "consumer-with-worker-rabbit-mq.csproj"
COPY . .
RUN dotnet build "consumer-with-worker-rabbit-mq.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "consumer-with-worker-rabbit-mq.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN sed -i 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/g' /etc/ssl/openssl.cnf

ENTRYPOINT ["dotnet", "consumer-with-worker-rabbit-mq.dll"]
