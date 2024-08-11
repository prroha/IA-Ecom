# Application Setup

## Setting Up SQL Server Using Docker
#### Run the following command to pull sqlserver image in docker
```
sudo docker pull mcr.microsoft.com/mssql/server:2022-latest
```
#### Run the following command to start a SQL Server container:
```bash
sudo docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Shoes@123' -p 1433:1433 --name sql_server_container -d mcr.microsoft.com/mssql/server:2022-latest
```
## Setting up the Application
Go to the application root with sln file
```cd IA-Ecom/IA-Ecom
Restore Dependencies:
```dotnet restore
Build the app for compile time errors
```dotnet build
Run the app
Go to the IA-ecom with .csproj file
```dotnet run
```
## Running in Different Environments

Basic Command: dotnet run

### Development Mode
#### On Unix-based systems (Linux, macOS):

```
ASPNETCORE_ENVIRONMENT=Development dotnet run``
```
#### On windows:
```
set ASPNETCORE_ENVIRONMENT=Development && dotnet run
```
### Production Mode
#### In linux
```
ASPNETCORE_ENVIRONMENT=Production dotnet run
```
#### On windows:
```
set ASPNETCORE_ENVIRONMENT=Production && dotnet run
```
### Adding Migrations to setup database tables
```
dotnet ef migrations add <migrationName>
```
### Running Migrations
```
dotnet ef database update
```
### Test Login Credentials
```
Admin:
Username/Email: admin@admin.com
Password: Admin@123

User:
Username: user@user.com 
Password: User@123
```