# NbtBlogEngine — Architecture Design Document

## 1. Overview

NbtBlogEngine is a lightweight blog engine built with **ASP.NET Web Forms (.NET Framework 4.8)**, **SQL Server**, and **Bootstrap 5**. It supports post creation, editing, publishing, tagging, commenting, user authentication, and a public-facing blog with search.

---

## 2. Technology Stack

| Layer | Technology |
|-------|-----------|
| Frontend | ASP.NET Web Forms, Bootstrap 5.2.3, jQuery 3.7.0 |
| Backend | C# (.NET Framework 4.8) |
| Database | SQL Server Express |
| ORM | ADO.NET (SqlHelper) |
| Authentication | ASP.NET Forms Authentication + Session |
| Static Analysis | StyleCop.Analyzers 1.1.118, FxCop Analyzers 3.3.0 |
| Testing | MSTest, Code Coverage via dotnet-coverage |

---

## 3. Architecture Pattern

The application follows a **Layered Architecture** with clear separation of concerns:

```
┌─────────────────────────────────────────────┐
│              UI Layer (ASPX Pages)           │
│  Default, Blog, PostDetails, Posts, NewPost  │
│  Login, Signup, About, Contact               │
├─────────────────────────────────────────────┤
│           Services Layer (Business Logic)    │
│  PostService, UserService, TagService,       │
│  CommentService                              │
│  ServiceFactory (Composition Root)           │
├─────────────────────────────────────────────┤
│         Repository Layer (Data Access)       │
│  IPostRepository    → PostRepository         │
│  IUserRepository    → UserRepository         │
│  ITagRepository     → TagRepository          │
│  ICommentRepository → CommentRepository      │
├─────────────────────────────────────────────┤
│         Data Layer (Infrastructure)          │
│  ISqlHelper → SqlHelper                      │
│  SqlQueries (Centralized SQL)                │
├─────────────────────────────────────────────┤
│              SQL Server Database             │
│  Tables: user, post, tag, post_tag,          │
│          post_comment                        │
│  Stored Proc: sp_CreatePost                  │
└─────────────────────────────────────────────┘
```

---

## 4. Folder Structure

```
NbtBlogEngine/
├── Models/                  # DTOs
│   ├── PostDTO.cs           # Blog post data
│   ├── UserDTO.cs           # User data
│   └── CommentDTO.cs        # Comment data
├── Helpers/                 # Pure utility classes
│   ├── SlugHelper.cs        # URL slug generation
│   ├── TextHelper.cs        # Text truncation
│   └── TagHelper.cs         # Tag badge HTML rendering
├── Repositories/            # Data access interfaces + implementations
│   ├── IPostRepository.cs
│   ├── IUserRepository.cs
│   ├── ITagRepository.cs
│   ├── ICommentRepository.cs
│   ├── PostRepository.cs
│   ├── UserRepository.cs
│   ├── TagRepository.cs
│   └── CommentRepository.cs
├── Services/                # Business logic
│   ├── PostService.cs
│   ├── UserService.cs
│   ├── TagService.cs
│   ├── CommentService.cs
│   └── ServiceFactory.cs    # Composition root
├── DataLayer/               # Infrastructure
│   ├── ISqlHelper           # DB abstraction interface
│   ├── SqlHelper.cs         # ADO.NET implementation
│   └── SqlQueries.cs        # Centralized SQL strings
├── Content/                 # CSS (Bootstrap + Site.css)
├── Scripts/                 # JS (Bootstrap + jQuery)
├── App_Start/               # BundleConfig, RouteConfig
├── *.aspx / *.aspx.cs       # WebForms pages
├── Site.Master              # Desktop master page
├── Site.Mobile.Master       # Mobile master page
├── Web.config               # Configuration
├── stylecop.json            # StyleCop settings
├── NbtBlogEngine.ruleset    # Analyzer rule suppressions
└── SQL/                     # Database scripts (sibling folder)
    ├── BlogDatabaseCreation.sql
    ├── Insert.sql
    ├── sp_CreatePost.sql
    └── sp_GetPostsByAuthor.sql

NbtBlogEngine.Tests/
├── SlugHelperTests.cs
├── TextHelperTests.cs
├── TagHelperTests.cs
├── PostServiceTests.cs
├── UserServiceTests.cs
├── TagServiceTests.cs
└── Mocks/
    ├── MockPostRepository.cs
    ├── MockUserRepository.cs
    └── MockTagRepository.cs
```

---

## 5. Database Schema

```
┌──────────┐     ┌──────────┐     ┌──────────┐
│   user   │     │   post   │     │   tag    │
├──────────┤     ├──────────┤     ├──────────┤
│ id (PK)  │◄────│ authorId │     │ id (PK)  │
│ firstName│     │ id (PK)  │     │ title    │
│ lastName │     │ title    │     └────┬─────┘
│ email    │     │ slug     │          │
│ password │     │ published│     ┌────┴─────┐
│ Hash     │     │ createdAt│     │ post_tag │
│registered│     │ updatedAt│     ├──────────┤
│ At       │     │ published│     │postId(FK)│
│ lastLogin│     │ At       │     │tagId (FK)│
└─────┬────┘     │ content  │     └──────────┘
      │          └──────────┘
      │
      │          ┌──────────────┐
      └─────────►│ post_comment │
                 ├──────────────┤
                 │ id (PK)      │
                 │ postId (FK)  │──► post.id
                 │ authorId(FK) │──► user.id
                 │ createdAt    │
                 │ content      │
                 └──────────────┘
```

### Relationships

| Relationship | Type | Description |
|-------------|------|-------------|
| user → post | 1:N | A user authors many posts |
| user → post_comment | 1:N | A user writes many comments |
| post → post_comment | 1:N | A post has many comments |
| post ↔ tag | N:M | Many-to-many via post_tag junction table |

---

## 6. Design Principles Applied

### 6.1 SOLID

| Principle | Implementation |
|-----------|---------------|
| **S** — Single Responsibility | Each service/repository handles one entity. Helpers are single-purpose utilities. |
| **O** — Open/Closed | New repositories (CommentRepository) added without modifying existing services. |
| **L** — Liskov Substitution | Mock repositories substitute real ones in tests seamlessly. |
| **I** — Interface Segregation | IPostRepository, IUserRepository, ITagRepository, ICommentRepository — each page depends only on what it needs. |
| **D** — Dependency Inversion | Services depend on repository interfaces, not concrete SQL implementations. |

### 6.2 Clean Code

- Meaningful variable and parameter names (`@PostId`, `@SearchTerm`, `@AuthorId`)
- Braces on all single-line conditions
- XML documentation on all public APIs
- No magic strings — SQL centralized in `SqlQueries.cs`
- No inline SQL in services or pages
- HTML-encoded user content to prevent XSS

### 6.3 Clean Architecture

- **Models** — no dependencies, pure data
- **Repositories** — depend only on DataLayer and Models
- **Services** — depend only on Repository interfaces and Helpers
- **UI Pages** — depend only on Services (via ServiceFactory)
- **No circular dependencies**

### 6.4 DRY

- `TextHelper.Truncate` — shared across Default, Blog pages
- `TagHelper.RenderBadges` — shared across Blog, PostDetails pages
- `SlugHelper.Generate` — called from PostService for both create and update
- `SqlQueries` — single source of truth for all SQL strings
- `ServiceFactory` — single composition root
- Author name resolution via `LEFT JOIN [user]` — reused in PostRepository and CommentRepository

---

## 7. Authentication & Authorization

```
┌─────────────────────────────────────────┐
│            Public Pages                  │
│  Home, Blog, PostDetails, About,         │
│  Contact, Login, Signup                  │
│  (No authentication required)            │
├─────────────────────────────────────────┤
│          Protected Actions               │
│  Posts (My Posts), NewPost               │
│  (Session["UserId"] required)            │
│  Redirects to ~/Login if not logged in   │
├─────────────────────────────────────────┤
│          Feature-Level Access            │
│  View comments: Everyone                 │
│  Post comments: Logged-in users only     │
│  Edit post link: Logged-in users only    │
├─────────────────────────────────────────┤
│          Navbar Visibility               │
│  Logged out: Home, Blog, About,          │
│              Contact, Login, Sign Up     │
│  Logged in:  Home, Blog, My Posts,       │
│              About, Contact, [Name],     │
│              Logout                      │
└─────────────────────────────────────────┘
```

- **Forms Authentication** configured in Web.config
- **Session** stores `UserId` and `UserName`
- **PostDetails** shows "Edit Post" link only for logged-in users
- **Comments** — form visible only for logged-in users; "Login to comment" shown for guests
- **Password** stored as plain hash (for demo; production should use bcrypt/PBKDF2)

---

## 8. Data Flow

### 8.1 Creating a Post

```
NewPost.aspx (UI)
    │
    ▼
NewPost.aspx.cs → ValidatePost()
    │
    ▼
PostService.CreatePost(authorId, title, content, published)
    │
    ├── SlugHelper.Generate(title) → "hello-world"
    │
    ▼
IPostRepository.Create(authorId, title, slug, content, published)
    │
    ▼
PostRepository → SqlQueries.Post.CreateStoredProc → sp_CreatePost
    │
    ▼
SQL Server → INSERT INTO post → SCOPE_IDENTITY()
    │
    ▼
TagService.SavePostTags(postId, tagIds)
    │
    ▼
ITagRepository.SavePostTags → DELETE old + INSERT new post_tag rows
```

### 8.2 Viewing the Blog

```
Blog.aspx (UI)
    │
    ▼
Blog.aspx.cs → Page_Load
    │
    ├── ?tag=C# → PostService.GetPublishedPostsByTag("C#")
    ├── ?q=AWS  → PostService.SearchPosts("AWS")
    └── (none)  → PostService.GetPublishedPosts()
    │
    ▼
IPostRepository → SqlQueries → SQL Server
    │
    ▼
DataTable → ListView binding
    │
    ▼
ItemDataBound → TagService.GetTagTitlesForPost(postId)
    │
    ▼
TagHelper.RenderBadges(titles) → HTML badge links
```

### 8.3 Posting a Comment

```
PostDetails.aspx (UI) → "Post Comment" button
    │
    ▼
PostDetails.aspx.cs → btnSubmitComment_Click
    │
    ├── Validate: comment not empty, user logged in
    │
    ▼
CommentService.AddComment(postId, authorId, content)
    │
    ▼
ICommentRepository.Create(postId, authorId, content)
    │
    ▼
CommentRepository → SqlQueries.Comment.Create
    │
    ▼
SQL Server → INSERT INTO post_comment (postId, authorId, ...) → SCOPE_IDENTITY()
    │
    ▼
LoadComments(postId) → CommentService.GetCommentsByPostId(postId)
    │
    ▼
CommentRepository → SqlQueries.Comment.SelectByPostId
    │
    ├── LEFT JOIN [user] u ON u.id = c.authorId
    │   → resolves firstName + lastName as authorName
    │
    ▼
List<CommentDTO> → Repeater binding
    │
    ▼
UI shows: [AuthorName]  [Date]
          [Comment text]
```

---

## 9. Page Map

| Page | URL | Access | Purpose |
|------|-----|--------|---------|
| Default.aspx | `/` | Public | Home page with hero + recent posts |
| Blog.aspx | `/Blog` | Public | Published posts with tags, search, tag filter |
| PostDetails.aspx | `/PostDetails?slug=x` | Public | Full post view with author, tags, comments |
| Posts.aspx | `/Posts` | Auth | Admin post list with search |
| NewPost.aspx | `/NewPost` | Auth | Create/edit/delete posts, manage tags |
| Login.aspx | `/Login` | Public | User login |
| Signup.aspx | `/Signup` | Public | User registration |
| About.aspx | `/About` | Public | About page |
| Contact.aspx | `/Contact` | Public | Contact info |

---

## 10. Key Features

| Feature | Implementation |
|---------|---------------|
| Post CRUD | PostService + PostRepository + sp_CreatePost |
| Auto-generated slugs | SlugHelper.Generate — lowercase, hyphenated, special chars removed |
| Tag management | Create, delete (if unused), assign to posts via CheckBoxList |
| Tag filtering | Blog.aspx?tag=C# → GetPublishedPostsByTag |
| Blog search | Blog.aspx?q=term → SearchPosts (published posts, title + content LIKE) |
| Admin search | Posts.aspx → SearchPosts (all posts by author) |
| Author name display | LEFT JOIN with user table in PostRepository.DetailSelect |
| Comments | CommentService + CommentRepository, author name via LEFT JOIN user |
| Comment access control | Logged-in users can post; guests see "Login to comment" |
| Content preview | TextHelper.Truncate(content, 250) on Blog/Home pages |
| Form validation | Client-side (RequiredFieldValidator) + server-side (ValidatePost) |
| Delete confirmation | JavaScript confirm() on Delete buttons |
| Responsive UI | Bootstrap 5 grid, desktop + mobile master pages |

---

## 11. Static Analysis

| Tool | Version | Purpose |
|------|---------|---------|
| StyleCop.Analyzers | 1.1.118 | Code style enforcement (naming, ordering, braces, spacing) |
| FxCop Analyzers | 3.3.0 | Code quality (design, globalization, security) |

Configuration files:
- `stylecop.json` — documentation rules disabled, using placement outside namespace
- `NbtBlogEngine.ruleset` — suppresses WebForms-specific false positives (CA1707, CA5368, SA1300, etc.)

---

## 12. Testing

| Test Class | Tests | Coverage |
|-----------|-------|----------|
| SlugHelperTests | 10 | 100% |
| TextHelperTests | 6 | 100% |
| TagHelperTests | 5 | 100% |
| PostServiceTests | 13 | 88% |
| UserServiceTests | 4 | 100% |
| TagServiceTests | 9 | 100% |
| **Total** | **47** | **Services + Helpers: ~95%** |

Mock repositories (`MockPostRepository`, `MockUserRepository`, `MockTagRepository`) enable testing without a database.

### Running Tests

```bash
# Run tests
vstest.console.exe bin\Debug\NbtBlogEngine.Tests.dll

# Run with code coverage
vstest.console.exe bin\Debug\NbtBlogEngine.Tests.dll --collect:"Code Coverage"

# Convert coverage to Cobertura XML
dotnet-coverage merge --output coverage.cobertura.xml --output-format cobertura TestResults\*.coverage
```

---

## 13. Build & Run

### Prerequisites
- Visual Studio 2022
- SQL Server Express
- .NET Framework 4.8

### Setup
1. Run `SQL/BlogDatabaseCreation.sql` to create the database and tables
2. Run `SQL/sp_CreatePost.sql` to create the stored procedure
3. Run `SQL/sp_GetPostsByAuthor.sql` to create the stored procedure
4. Run `SQL/Insert.sql` to seed sample data (3 users, 10 tags, 10 posts, post-tag links, 5 comments)
5. Update connection string in `Web.config` if needed
6. Build and run from Visual Studio (F5)

### Build from Command Line
```bash
"C:\Program Files\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin\amd64\MSBuild.exe" NbtBlogEngine.csproj /t:Rebuild /p:Configuration=Debug
```

---

## 14. Configuration

| File | Purpose |
|------|---------|
| `Web.config` | Connection string, Forms Auth, session timeout, validation mode, compiler settings |
| `Bundle.config` | CSS bundle definition |
| `stylecop.json` | StyleCop analyzer settings |
| `NbtBlogEngine.ruleset` | Analyzer rule suppressions |
| `packages.config` | NuGet package references |

---

## 15. Security Considerations

| Area | Current State | Recommendation for Production |
|------|--------------|-------------------------------|
| Password storage | Plain hash in DB | Use bcrypt or PBKDF2 |
| SQL injection | Parameterized queries throughout | ✅ Already secure |
| XSS | Server.HtmlEncode on comment/tag output | Add Content-Security-Policy header |
| CSRF | WebForms ViewState provides basic protection | Add anti-forgery tokens |
| Session | 30-minute timeout | Add secure/httponly cookie flags |
| HTTPS | Not enforced | Enable requireSSL in Forms Auth |
| Comment spam | No protection | Add CAPTCHA or rate limiting |

---

## 16. Future Enhancements

- Rich text editor (TinyMCE/CKEditor) for post content
- Image upload for posts
- Role-based authorization (Admin vs Author)
- Password hashing with bcrypt
- Email verification on signup
- RSS feed generation
- SEO meta tags per post
- Comment moderation (approve/reject)
- Comment reply threading
- User profile page
