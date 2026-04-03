# Coding Standards

This document defines the coding conventions for the NbtBlogEngine project. Standards are enforced by **StyleCop.Analyzers** and **FxCop Analyzers** — the build target is zero warnings.

## Naming Conventions

| Element | Style | Example |
|---------|-------|---------|
| Class / Interface | PascalCase | `PostService`, `IPostRepository` |
| Public method / property | PascalCase | `CreatePost()`, `Title` |
| Private field | `_camelCase` | `_repo`, `_connectionString` |
| Parameter / local variable | camelCase | `authorId`, `slug` |
| Constant | PascalCase | `SelectByAuthor` |
| Interface prefix | `I` | `IPostRepository`, `ISqlHelper` |
| DTO suffix | `DTO` | `PostDTO`, `UserDTO`, `CommentDTO` |

## File & Class Organization

- One public class per file (exception: `ISqlHelper` + `SqlHelper` colocated in `SqlHelper.cs`)
- `using` directives placed **outside** the namespace
- Order within a class:
  1. Fields
  2. Constructors
  3. Public methods
  4. Private methods

## Code Style

### Braces & Formatting

- Always use braces for `if`, `else`, `for`, `foreach`, `while` — even single-line bodies:
  ```csharp
  // ✅ Correct
  if (string.IsNullOrWhiteSpace(title))
  {
      return string.Empty;
  }

  // ❌ Wrong
  if (string.IsNullOrWhiteSpace(title))
      return string.Empty;
  ```
- Opening brace on its own line (Allman style)
- Expression-bodied members allowed for single-line methods:
  ```csharp
  public DataTable GetPublishedPosts() => _repo.GetPublished();
  ```

### Null Handling

- Use `ArgumentNullException` for required constructor parameters:
  ```csharp
  _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
  ```
- Use `?.` and `??` operators where appropriate

### String Handling

- Use `string.IsNullOrWhiteSpace()` over `string.IsNullOrEmpty()` for user input
- Use `ToLowerInvariant()` instead of `ToLower()` for culture-independent operations

## SQL & Data Access

### All SQL in SqlQueries.cs

All SQL strings must be defined as `const` in the `SqlQueries` static class. No inline SQL in services, repositories, or pages:

```csharp
// ✅ Correct — in SqlQueries.cs
public const string FindById = DetailSelect + " WHERE p.id=@PostId";

// ✅ Correct — in Repository
_db.ExecuteReader(SqlQueries.Post.FindById, CommandType.Text, new SqlParameter("@PostId", postId));

// ❌ Wrong — inline SQL in repository
_db.ExecuteReader("SELECT * FROM post WHERE id=@PostId", ...);
```

### Parameterized Queries

Always use `SqlParameter` — never concatenate user input into SQL:

```csharp
// ✅ Correct
new SqlParameter("@AuthorId", authorId)

// ❌ Wrong
"SELECT * FROM post WHERE authorId=" + authorId
```

### Stored Procedures

Reference stored procedure names as constants in `SqlQueries`:

```csharp
public const string CreateStoredProc = "sp_CreatePost";
```

Call with `CommandType.StoredProcedure`:

```csharp
_db.ExecuteScalar(SqlQueries.Post.CreateStoredProc, CommandType.StoredProcedure, parameters);
```

## Architecture Rules

### Layer Dependencies

```
Pages (.aspx.cs) → Services (via ServiceFactory) → Repositories (via interfaces) → DataLayer (ISqlHelper)
```

- Pages must not access repositories or `SqlHelper` directly
- Services depend on repository **interfaces**, not concrete classes
- Repositories depend on `ISqlHelper`, not `SqlHelper`
- Models (DTOs) have no dependencies

### Adding a New Entity

1. Create `Models/EntityDTO.cs`
2. Create `Repositories/IEntityRepository.cs` (interface)
3. Create `Repositories/EntityRepository.cs` (implementation taking `ISqlHelper`)
4. Add SQL strings to `SqlQueries.EntityName` nested class
5. Create `Services/EntityService.cs` (taking `IEntityRepository`)
6. Register in `ServiceFactory`:
   ```csharp
   public static EntityService CreateEntityService() => new EntityService(new EntityRepository(CreateDb()));
   ```
7. Create `Tests/Mocks/MockEntityRepository.cs`
8. Create `Tests/EntityServiceTests.cs`

## Security

- HTML-encode all user-generated content before rendering: `Server.HtmlEncode(value)`
- Use `Forms Authentication` for protected pages — check `Session["UserId"]` for authorization
- Never log or expose connection strings, passwords, or stack traces to the UI

## XML Documentation

XML doc comments are required on all **public** members in Services, Repositories, Helpers, and Models:

```csharp
/// <summary>Gets all posts by the specified author.</summary>
/// <param name="authorId">The author's unique identifier.</param>
/// <returns>A <see cref="DataTable"/> containing the author's posts.</returns>
public DataTable GetPostsByAuthor(int authorId) => _repo.GetByAuthor(authorId);
```

Documentation is **not** required on:
- Private members
- Interface members (documented on implementation)
- Page code-behind files (`.aspx.cs`)

## Analyzer Configuration

| File | Purpose |
|------|---------|
| `stylecop.json` | StyleCop settings — documentation rules, using placement, naming |
| `.editorconfig` | Analyzer severity overrides |
| `NbtBlogEngine.ruleset` | Rule suppressions for WebForms-specific false positives |

### Suppressed Rules (with reasons)

| Rule | Reason |
|------|--------|
| SA1101 | `this.` prefix not required — `_camelCase` fields are clear enough |
| SA1309 | Private fields use `_` prefix (project convention) |
| SA1402 | `ISqlHelper` + `SqlHelper` colocated intentionally |
| SA1600–SA1602 | Documentation enforced selectively, not on every element |
| SA1633 | No file header comments required |
| CA1034 | Nested classes used for `SqlQueries` organization |
| CA2000 | `SqlConnection` lifetime managed by `CommandBehavior.CloseConnection` |
| CA5368 | `HttpOnly` cookie flag handled at IIS level |
