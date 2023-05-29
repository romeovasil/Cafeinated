FROM mcr.microsoft.com/dotnet/sdk:6.0 as builder
WORKDIR /app/

COPY **/Cafeinated.Backend.App.csproj /app/Cafeinated.Backend.App/
COPY **/Cafeinated.Backend.Core.csproj /app/Cafeinated.Backend.Core/
COPY **/Cafeinated.Backend.Infrastructure.csproj /app/Cafeinated.Backend.Infrastructure/
COPY **/Cafeinated.Backend.Tests.csproj /app/Cafeinated.Backend.Tests/
COPY *.sln /app/

RUN dotnet restore

COPY ./ /app

RUN dotnet publish --output "/app/bin" -c release 

FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app/bin

COPY --from=builder /app/bin /app/bin

CMD ["dotnet", "Cafeinated.Backend.App.dll"]

