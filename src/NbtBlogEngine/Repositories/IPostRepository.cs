using System.Data;
using NbtBlogEngine.Models;

namespace NbtBlogEngine.Repositories
{
    /// <summary>
    /// Defines data access operations for blog posts.
    /// </summary>
    public interface IPostRepository
    {
        /// <summary>Gets all posts by the specified author.</summary>
        /// <param name="authorId">The author's unique identifier.</param>
        /// <returns>A <see cref="DataTable"/> containing the author's posts.</returns>
        DataTable GetByAuthor(int authorId);

        /// <summary>Creates a new post.</summary>
        /// <param name="authorId">The author's unique identifier.</param>
        /// <param name="title">The post title.</param>
        /// <param name="slug">The URL-friendly slug.</param>
        /// <param name="content">The post body content.</param>
        /// <param name="published">Whether the post is published immediately.</param>
        /// <returns>The unique identifier of the newly created post.</returns>
        long Create(int authorId, string title, string slug, string content, bool published);

        /// <summary>Updates an existing post.</summary>
        /// <param name="postId">The post's unique identifier.</param>
        /// <param name="authorId">The author's unique identifier.</param>
        /// <param name="title">The updated title.</param>
        /// <param name="slug">The updated slug.</param>
        /// <param name="content">The updated content.</param>
        /// <param name="published">Whether the post is published.</param>
        /// <returns><c>true</c> if the post was updated; otherwise <c>false</c>.</returns>
        bool Update(long postId, int authorId, string title, string slug, string content, bool published);

        /// <summary>Finds a post by its unique identifier.</summary>
        /// <param name="postId">The post's unique identifier.</param>
        /// <returns>The <see cref="PostDTO"/> if found; otherwise <c>null</c>.</returns>
        PostDTO FindById(long postId);

        /// <summary>Finds a post by its URL slug.</summary>
        /// <param name="slug">The URL-friendly slug.</param>
        /// <returns>The <see cref="PostDTO"/> if found; otherwise <c>null</c>.</returns>
        PostDTO FindBySlug(string slug);

        /// <summary>Deletes a post by its unique identifier.</summary>
        /// <param name="postId">The post's unique identifier.</param>
        /// <returns><c>true</c> if the post was deleted; otherwise <c>false</c>.</returns>
        bool Delete(long postId);

        /// <summary>Gets all published posts ordered by publish date descending.</summary>
        /// <returns>A <see cref="DataTable"/> containing published posts.</returns>
        DataTable GetPublished();

        /// <summary>Gets all published posts that have the specified tag.</summary>
        /// <param name="tagTitle">The tag title to filter by.</param>
        /// <returns>A <see cref="DataTable"/> containing matching published posts.</returns>
        DataTable GetPublishedByTag(string tagTitle);

        /// <summary>Searches posts by title or content.</summary>
        /// <param name="searchTerm">The search term to match against title and content.</param>
        /// <returns>A <see cref="DataTable"/> containing matching posts.</returns>
        DataTable Search(string searchTerm);
    }
}
