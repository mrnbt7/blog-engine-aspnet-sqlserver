using System.Collections.Generic;
using NbtBlogEngine.Models;
using NbtBlogEngine.Repositories;

namespace NbtBlogEngine.Services
{
    /// <summary>
    /// Provides business logic for post comment operations.
    /// </summary>
    public class CommentService
    {
        private readonly ICommentRepository _repo;

        /// <summary>Initializes a new instance of the <see cref="CommentService"/> class.</summary>
        /// <param name="repo">The comment repository for data access.</param>
        public CommentService(ICommentRepository repo)
        {
            _repo = repo;
        }

        /// <summary>Gets all comments for a post with author names.</summary>
        /// <param name="postId">The post's unique identifier.</param>
        /// <returns>A list of comments ordered by creation date.</returns>
        public List<CommentDTO> GetCommentsByPostId(long postId) => _repo.GetByPostId(postId);

        /// <summary>Adds a new comment to a post.</summary>
        /// <param name="postId">The post's unique identifier.</param>
        /// <param name="authorId">The commenter's user ID.</param>
        /// <param name="content">The comment text.</param>
        /// <returns>The unique identifier of the newly created comment.</returns>
        public long AddComment(long postId, long authorId, string content) => _repo.Create(postId, authorId, content);

        /// <summary>Gets the total number of comments for a post.</summary>
        /// <param name="postId">The post's unique identifier.</param>
        /// <returns>The comment count.</returns>
        public int GetCommentCount(long postId) => _repo.GetCountByPostId(postId);
    }
}
