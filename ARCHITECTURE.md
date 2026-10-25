# Backend architecture

The project uses feature modules inspired by NestJS. `Program.cs` is only the
composition root. Business code belongs under `Modules`; cross-cutting and
infrastructure code stays at the project root.

## Modules

- `Modules/Users`: User controller, service, request/response DTOs, validators, and mapping.
- `Modules/Blogs`: Blog controller, service, request/response DTOs, validators, and mapping.
- `Entities`: Shared business data types used by feature modules, repository contracts,
  and database implementations. Entities are plain C# types and do not require
  dependency-injection registration.
- `Common`: Cross-cutting infrastructure such as guards, middleware, exceptions,
  and request context.
- `Shared`: Small stateless and infrastructure-independent helpers, such as token
  generation. Shared must not depend on feature modules or repositories.
- `Database`: The EF Core `DbContext`, entity mappings, database configuration,
  and generated migrations.
- `Repositories`: Persistence contracts and implementations. API services use
  repository interfaces rather than opening database connections directly.

## Dependency direction

```text
Modules ---------> Repositories ---------> Database
   |                    |                     |
   +--------------------+---------------------+
                        v
                     Entities

Modules ----------> Common
Modules ----------> Shared
```

Keep user-specific logic inside `Modules/Users` and blog-specific logic inside
`Modules/Blogs`. Put only shared business data definitions in `Entities`; do not
place controller, service, database, or feature-specific workflow logic there.
Do not place feature-specific behavior in `Common` or `Shared`.

## Database setup

The PostgreSQL schema is defined by the entities in `Entities` and the EF Core
Fluent API mappings in `Database/Configurations`. EF Core migrations are the
single source of truth for applying schema changes; do not maintain a second set
of handwritten schema migrations.

The connection wrapper expects these settings once a provider is selected:

```json
{
  "Database": {
    "ConnectionString": "stored outside source control"
  }
}
```

Supply the connection string through the `Database__ConnectionString`
environment variable or .NET user secrets. After installing the .NET SDK,
restore the project-local `dotnet-ef` tool, then create and apply the initial
migration with:

```bash
set -a
source .env
set +a

dotnet tool restore
dotnet ef migrations add InitialCreate --output-dir Database/Migrations
dotnet ef database update
```

The local `.env` file is ignored by Git. ASP.NET Core does not load `.env`
files automatically; the shell commands above export its values before running
the application or EF Core commands. Commit `.env.example`, never `.env`.
