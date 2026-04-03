using System.Collections.Generic;
using NbtBlogEngine.Models;

namespace NbtBlogEngine.Repositories
{
    /// <summary>
    /// Defines data access operations for post comments.
    /// </summary>
    public interface ICommentRepository
    {
        /// <summary>Gets all comments for a post with author names, ordered by creation date.</summary>
        /// <param name="postId">The post's unique identifier.</param>
        /// <returns>A list of comments for the post.</returns>
        List<CommentDTO> GetByPostId(long postId);

        /// <summary>Creates a new comment on a post.</summary>
        /// <param name="postId">The post's unique identifier.</param>
        /// <param name="authorId">The commenter's user ID.</param>
        /// <param name="content">The comment text.</param>
        /// <returns>The unique identifier of the newly created comment.</returns>
        long Create(long postId, long authorId, string content);

        /// <summary>Gets the total number of comments for a post.</summary>
        /// <param name="postId">The post's unique identifier.</param>
        /// <returns>The comment count.</returns>
        int GetCountByPostId(long postId);
    }
}
