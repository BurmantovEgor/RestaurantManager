FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# �������� ����� �������
COPY ApiGateway.csproj ./
RUN dotnet restore

# �������� ���� ��� � �������� ������
COPY . .  
RUN dotnet publish -c Release -o /app/publish

# ���������� ����������� runtime-����� ��� �������
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# ��������� ����������
ENTRYPOINT ["dotnet", "ApiGateway.dll"]
