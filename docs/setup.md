# Setup Guide

## Prerequisites

- **Visual Studio 2019+** (with ASP.NET and web development workload)
- **.NET Framework 4.8** Developer Pack
- **SQL Server Express** (LocalDB or named instance)
  - Connection String: `Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;`

## Database Setup

1. Open **SQL Server Management Studio** (SSMS) or any SQL client and connect to your SQL Server Express instance.
2. Run the SQL scripts in the following order:
   ```
   sql/BlogDatabaseCreation.sql
   sql/sp_GetPostsByAuthor.sql
   sql/Insert.sql              (optional - sample data)
   ```

## Configure Connection String

Update the connection string in `src/NbtBlogEngine/Web.config` to match your SQL Server instance:

```xml
<connectionStrings>
  <add name="SqlDbConnection"
       connectionString="Data Source=localhost\SQLEXPRESS;Initial Catalog=blog;Trusted_Connection=True"
       providerName="System.Data.SqlClient"/>
</connectionStrings>
```

Replace `localhost\SQLEXPRESS` with your server instance name.

## Build & Run

1. Open `src/NbtBlogEngine/NbtBlogEngine.sln` in Visual Studio.
2. Right-click the solution → **Restore NuGet Packages**.
3. Build the solution (`Ctrl+Shift+B`).
4. Press `F5` to run with IIS Express.

## Running Tests

1. Open **Test Explorer** (`Ctrl+E, T`).
2. Click **Run All** to execute unit tests from the `NbtBlogEngine.Tests` project.

## Static Analysis (StyleCop & FxCop)

The project uses **StyleCop.Analyzers 1.1.118** and **FxCop Analyzers 3.3.0** which run automatically during build. No separate installation is needed — they are included as NuGet packages.

### In Visual Studio

1. Build the solution (`Ctrl+Shift+B`) — analyzer warnings appear in the **Error List** window.
2. To see only analyzer warnings: filter Error List by **Project → NbtBlogEngine** and look for `SA` (StyleCop) and `CA` (FxCop) codes.

### From Command Line

```bash
msbuild NbtBlogEngine.csproj /t:Rebuild /p:Configuration=Debug
```

Analyzer warnings will appear in the build output prefixed with `SA` or `CA` codes.

### Configuration Files

| File | Purpose |
|------|---------|
| `stylecop.json` | StyleCop settings (documentation rules, using placement, naming) |
| `.editorconfig` | Analyzer rule severity overrides (suppressed rules) |
| `NbtBlogEngine.ruleset` | Additional rule suppressions for WebForms-specific false positives |

The build target is **zero warnings**. Any new code must not introduce StyleCop or FxCop warnings.

## Project Structure

```
NbtBlogEngine/
├── docs/                    # Architecture documentation
├── sql/                     # Database scripts
└── src/
    ├── NbtBlogEngine/       # Main web application (ASP.NET Web Forms)
    │   ├── DataLayer/       # SQL helper and queries
    │   ├── Models/          # DTOs
    │   ├── Repositories/    # Data access interfaces and implementations
    │   ├── Services/        # Business logic
    │   └── Helpers/         # Utility classes
    └── NbtBlogEngine.Tests/ # Unit tests (MSTest)
        └── Mocks/           # Mock repositories
```
