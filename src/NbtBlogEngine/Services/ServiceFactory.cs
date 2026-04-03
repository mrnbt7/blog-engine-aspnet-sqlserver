using NbtBlogEngine.DataLayer;
using NbtBlogEngine.Repositories;

namespace NbtBlogEngine.Services
{
    /// <summary>
    /// Composition root that creates service instances with their dependencies wired up.
    /// </summary>
    public static class ServiceFactory
    {
        /// <summary>Creates a new <see cref="PostService"/> with its repository dependency.</summary>
        /// <returns>A configured <see cref="PostService"/> instance.</returns>
        public static PostService CreatePostService() => new PostService(new PostRepository(CreateDb()));

        /// <summary>Creates a new <see cref="UserService"/> with its repository dependency.</summary>
        /// <returns>A configured <see cref="UserService"/> instance.</returns>
        public static UserService CreateUserService() => new UserService(new UserRepository(CreateDb()));

        /// <summary>Creates a new <see cref="TagService"/> with its repository dependency.</summary>
        /// <returns>A configured <see cref="TagService"/> instance.</returns>
        public static TagService CreateTagService() => new TagService(new TagRepository(CreateDb()));

        /// <summary>Creates a new <see cref="CommentService"/> with its repository dependency.</summary>
        /// <returns>A configured <see cref="CommentService"/> instance.</returns>
        public static CommentService CreateCommentService() => new CommentService(new CommentRepository(CreateDb()));

        /// <summary>Creates a new SQL helper instance for database access.</summary>
        /// <returns>An <see cref="ISqlHelper"/> configured with the application connection string.</returns>
        private static ISqlHelper CreateDb() => new SqlHelper();
    }
}
