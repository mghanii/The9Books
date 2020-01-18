#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.


FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS builder
WORKDIR /source


COPY *.csproj .
RUN dotnet restore

COPY . ./

RUN dotnet publish "./The9Books.csproj" --output "./dist" --configuration Release --no-restore

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim
WORKDIR /app
COPY --from=builder /source/dist .
COPY --from=builder /source/Data ./Data
EXPOSE 80
ENTRYPOINT ["dotnet", "The9Books.dll"]