using System.Data;
using NbtBlogEngine.Helpers;
using NbtBlogEngine.Models;
using NbtBlogEngine.Repositories;

namespace NbtBlogEngine.Services
{
    /// <summary>
    /// Provides business logic for blog post operations.
    /// Automatically generates URL slugs from post titles.
    /// </summary>
    public class PostService
    {
        private readonly IPostRepository _repo;

        /// <summary>Initializes a new instance of the <see cref="PostService"/> class.</summary>
        /// <param name="repo">The post repository for data access.</param>
        public PostService(IPostRepository repo)
        {
            _repo = repo;
        }

        /// <summary>Gets all posts by the specified author.</summary>
        /// <param name="authorId">The author's unique identifier.</param>
        /// <returns>A <see cref="DataTable"/> containing the author's posts.</returns>
        public DataTable GetPostsByAuthor(int authorId) => _repo.GetByAuthor(authorId);

        /// <summary>Creates a new post with an auto-generated slug.</summary>
        /// <param name="authorId">The author's unique identifier.</param>
        /// <param name="title">The post title.</param>
        /// <param name="content">The post body content.</param>
        /// <param name="published">Whether to publish the post immediately.</param>
        /// <returns>The unique identifier of the newly created post.</returns>
        public long CreatePost(int authorId, string title, string content, bool published)
        {
            return _repo.Create(authorId, title, SlugHelper.Generate(title), content, published);
        }

        /// <summary>Updates an existing post with an auto-generated slug.</summary>
        /// <param name="postId">The post's unique identifier.</param>
        /// <param name="authorId">The author's unique identifier.</param>
        /// <param name="title">The updated title.</param>
        /// <param name="content">The updated content.</param>
        /// <param name="published">Whether the post is published.</param>
        /// <returns><c>true</c> if the post was updated; otherwise <c>false</c>.</returns>
        public bool UpdatePost(long postId, int authorId, string title, string content, bool published)
        {
            return _repo.Update(postId, authorId, title, SlugHelper.Generate(title), content, published);
        }

        /// <summary>Gets a post by its unique identifier.</summary>
        /// <param name="postId">The post's unique identifier.</param>
        /// <returns>The <see cref="PostDTO"/> if found; otherwise <c>null</c>.</returns>
        public PostDTO GetPostById(long postId) => _repo.FindById(postId);

        /// <summary>Gets a post by its URL slug.</summary>
        /// <param name="slug">The URL-friendly slug.</param>
        /// <returns>The <see cref="PostDTO"/> if found; otherwise <c>null</c>.</returns>
        public PostDTO GetPostBySlug(string slug) => _repo.FindBySlug(slug);

        /// <summary>Deletes a post by its unique identifier.</summary>
        /// <param name="postId">The post's unique identifier.</param>
        /// <returns><c>true</c> if the post was deleted; otherwise <c>false</c>.</returns>
        public bool DeletePost(long postId) => _repo.Delete(postId);

        /// <summary>Gets all published posts.</summary>
        /// <returns>A <see cref="DataTable"/> containing published posts.</returns>
        public DataTable GetPublishedPosts() => _repo.GetPublished();

        /// <summary>Gets all published posts filtered by tag.</summary>
        /// <param name="tagTitle">The tag title to filter by.</param>
        /// <returns>A <see cref="DataTable"/> containing matching published posts.</returns>
        public DataTable GetPublishedPostsByTag(string tagTitle) => _repo.GetPublishedByTag(tagTitle);

        /// <summary>Searches posts by title or content.</summary>
        /// <param name="searchTerm">The search term.</param>
        /// <returns>A <see cref="DataTable"/> containing matching posts.</returns>
        public DataTable SearchPosts(string searchTerm) => _repo.Search(searchTerm);
    }
}
