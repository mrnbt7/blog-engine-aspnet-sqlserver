using System;

namespace NbtBlogEngine.Models
{
    /// <summary>
    /// Data transfer object representing a post comment.
    /// </summary>
    public class CommentDTO
    {
        /// <summary>Gets or sets the unique identifier of the comment.</summary>
        public long Id { get; set; }

        /// <summary>Gets or sets the post ID this comment belongs to.</summary>
        public long PostId { get; set; }

        /// <summary>Gets or sets the author's unique identifier.</summary>
        public long AuthorId { get; set; }

        /// <summary>Gets or sets the full name of the commenter (resolved from user table).</summary>
        public string AuthorName { get; set; }

        /// <summary>Gets or sets the comment text.</summary>
        public string Content { get; set; }

        /// <summary>Gets or sets the date and time the comment was created.</summary>
        public DateTime CreatedAt { get; set; }
    }
}
