namespace NbtBlogEngine.DataLayer
{
    /// <summary>
    /// Centralized store for all SQL query strings used by the repositories.
    /// </summary>
    public static class SqlQueries
    {
        /// <summary>SQL queries for the post table.</summary>
        public static class Post
        {
            /// <summary>Selects posts by author ID, ordered by creation date descending.</summary>
            public const string SelectByAuthor =
                "SELECT id, createdAt, title, slug, published FROM post WHERE authorId=@AuthorId ORDER BY createdAt DESC";

            /// <summary>Updates a post's fields by post ID.</summary>
            public const string Update = @"
                UPDATE post SET authorId=@AuthorId, title=@Title, slug=@Slug, content=@Content,
                    published=@Published, updatedAt=GETDATE(),
                    publishedAt = CASE WHEN @Published=1 AND publishedAt IS NULL THEN GETDATE() ELSE publishedAt END
                WHERE id=@PostId";

            /// <summary>Base SELECT for post detail with author name join.</summary>
            public const string DetailSelect =
                "SELECT p.id, p.authorId, p.title, p.slug, p.content, p.published, p.createdAt, " +
                "ISNULL(u.firstName,'') + ' ' + ISNULL(u.lastName,'') AS authorName " +
                "FROM post p LEFT JOIN [user] u ON u.id=p.authorId";

            /// <summary>Finds a single post by ID with author name.</summary>
            public const string FindById = DetailSelect + " WHERE p.id=@PostId";

            /// <summary>Finds a single post by slug with author name.</summary>
            public const string FindBySlug = DetailSelect + " WHERE p.slug=@Slug";

            /// <summary>Deletes a post by ID.</summary>
            public const string Delete = "DELETE FROM post WHERE id=@PostId";

            /// <summary>Selects all published posts ordered by publish date descending.</summary>
            public const string SelectPublished =
                "SELECT id, title, slug, content, publishedAt FROM post WHERE published=1 ORDER BY publishedAt DESC";

            /// <summary>Selects published posts filtered by tag title.</summary>
            public const string SelectPublishedByTag = @"
                SELECT p.id, p.title, p.slug, p.content, p.publishedAt
                FROM post p INNER JOIN post_tag pt ON pt.postId=p.id INNER JOIN tag t ON t.id=pt.tagId
                WHERE p.published=1 AND t.title=@TagTitle ORDER BY p.publishedAt DESC";

            /// <summary>Searches published posts by title or content using LIKE.</summary>
            public const string Search =
                "SELECT id, title, slug, content, publishedAt FROM post WHERE published=1 AND (title LIKE @SearchTerm OR content LIKE @SearchTerm) ORDER BY publishedAt DESC";

            /// <summary>Name of the stored procedure for creating a post.</summary>
            public const string CreateStoredProc = "sp_CreatePost";
        }

        /// <summary>SQL queries for the user table.</summary>
        public static class User
        {
            /// <summary>Finds a user by email and password hash.</summary>
            public const string FindByCredentials =
                "SELECT id, firstName, email FROM [user] WHERE email=@Email AND passwordHash=@Password";

            /// <summary>Finds a user by ID with post count.</summary>
            public const string FindById =
                "SELECT u.id, u.firstName, u.lastName, u.email, u.registeredAt, u.lastLogin, " +
                "(SELECT COUNT(*) FROM post WHERE authorId=u.id) AS postCount " +
                "FROM [user] u WHERE u.id=@UserId";

            /// <summary>Updates a user's profile fields.</summary>
            public const string Update =
                "UPDATE [user] SET firstName=@FirstName, lastName=@LastName, email=@Email WHERE id=@UserId";

            /// <summary>Inserts a new user record.</summary>
            public const string Create =
                "INSERT INTO [user] (firstName,lastName,email,passwordHash,registeredAt) VALUES (@FirstName,@LastName,@Email,@Password,GETDATE())";
        }

        /// <summary>SQL queries for the tag and post_tag tables.</summary>
        public static class Tag
        {
            /// <summary>Selects all tags ordered by title.</summary>
            public const string SelectAll = "SELECT id, title FROM tag ORDER BY title";

            /// <summary>Selects tag IDs linked to a post.</summary>
            public const string SelectIdsForPost = "SELECT tagId FROM post_tag WHERE postId=@PostId";

            /// <summary>Selects tag titles linked to a post.</summary>
            public const string SelectTitlesForPost =
                "SELECT t.title FROM tag t INNER JOIN post_tag pt ON pt.tagId=t.id WHERE pt.postId=@PostId ORDER BY t.title";

            /// <summary>Deletes all post-tag links for a post.</summary>
            public const string DeletePostTags = "DELETE FROM post_tag WHERE postId=@PostId";

            /// <summary>Inserts a single post-tag link.</summary>
            public const string InsertPostTag = "INSERT INTO post_tag (postId,tagId) VALUES (@PostId,@TagId)";

            /// <summary>Inserts a new tag and returns its ID.</summary>
            public const string Create = "INSERT INTO tag (title) VALUES (@Title); SELECT SCOPE_IDENTITY();";

            /// <summary>Deletes a tag only if it is not linked to any post.</summary>
            public const string Delete = "DELETE FROM tag WHERE id=@TagId AND NOT EXISTS (SELECT 1 FROM post_tag WHERE tagId=@TagId)";
        }

        /// <summary>SQL queries for the post_comment table.</summary>
        public static class Comment
        {
            /// <summary>Selects all comments for a post with author name, ordered by creation date.</summary>
            public const string SelectByPostId =
                "SELECT c.id, c.postId, c.authorId, c.content, c.createdAt, " +
                "ISNULL(u.firstName,'') + ' ' + ISNULL(u.lastName,'') AS authorName " +
                "FROM post_comment c LEFT JOIN [user] u ON u.id=c.authorId " +
                "WHERE c.postId=@PostId ORDER BY c.createdAt ASC";

            /// <summary>Inserts a new comment and returns its ID.</summary>
            public const string Create =
                "INSERT INTO post_comment (postId, authorId, createdAt, content) VALUES (@PostId, @AuthorId, GETDATE(), @Content); SELECT SCOPE_IDENTITY();";

            /// <summary>Gets the comment count for a post.</summary>
            public const string CountByPostId =
                "SELECT COUNT(*) FROM post_comment WHERE postId=@PostId";
        }
    }
}
