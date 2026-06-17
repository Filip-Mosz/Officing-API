# Officing API

System rezerwacji przestrzeni biurowej wykonany w ASP.NET Core Web API.

## Technologies

- ASP.NET Core 8
- Entity Framework Core
- SQLite
- AutoMapper
- Swagger

## Additional Features

### Pagination

Example:

GET /api/workspaces?pageNumber=1&pageSize=10

### Soft Delete

Deleted workspaces are marked as deleted and hidden from API responses.

## Run

Restore packages:

```bash
dotnet restore
```

Apply migrations:

```bash
dotnet ef database update
```

Run application:

```bash
dotnet run
```

## Swagger

Available after startup:

```txt
https://localhost:7059/swagger
```