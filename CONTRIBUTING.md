# Contributing

## Getting Started

1. Fork the repository and clone it locally.
2. Follow [SETUP.md](SETUP.md) to set up the project.
3. Create a branch, make your changes, and submit a pull request.

## Branch Naming

| Type | Format | Example |
|------|--------|---------|
| Feature | `feature/short-description` | `feature/comment-replies` |
| Bug fix | `bugfix/short-description` | `bugfix/login-redirect` |
| Refactor | `refactor/short-description` | `refactor/sql-helper` |

## Commit Messages

Use clear, concise commit messages:

```
Add comment reply threading
Fix null check in PostService.CreatePost
Refactor SqlHelper to use async/await
```

- Start with a verb (Add, Fix, Update, Remove, Refactor)
- Keep the subject line under 72 characters

## Coding Standards

The project uses **StyleCop.Analyzers** and **FxCop Analyzers** for enforcement. Build must produce zero warnings.

- Use `_camelCase` for private fields
- Use `PascalCase` for public members, classes, and methods
- Place `using` directives outside the namespace
- Use braces on all `if`/`else`/`for`/`while` blocks, even single-line
- Keep SQL strings in `SqlQueries.cs` — no inline SQL in services or pages
- Use parameterized queries — never concatenate user input into SQL
- HTML-encode all user-generated content displayed on pages

## Architecture Rules

- **Pages** (`.aspx.cs`) should only call Services via `ServiceFactory` — no direct repository or SQL access
- **Services** depend on repository interfaces, not concrete implementations
- **Repositories** depend on `ISqlHelper`, not `SqlHelper` directly
- New entities need: DTO → Interface → Repository → Service → register in `ServiceFactory`

## Testing

- Write unit tests for all new service and helper methods
- Use mock repositories (see `NbtBlogEngine.Tests/Mocks/`) — no database dependency in tests
- Run all tests before submitting a PR:
  - Visual Studio: Test Explorer → Run All
- All existing tests must pass

## Pull Request Process

1. Ensure the solution builds with zero warnings.
2. Ensure all tests pass.
3. Describe what your PR does and why.
4. Keep PRs focused — one feature or fix per PR.
