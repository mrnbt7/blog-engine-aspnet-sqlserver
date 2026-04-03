using System;

namespace NbtBlogEngine.Models
{
    /// <summary>
    /// Data transfer object representing a blog post.
    /// </summary>
    public class PostDTO
    {
        /// <summary>Gets or sets the unique identifier of the post.</summary>
        public long Id { get; set; }

        /// <summary>Gets or sets the author's unique identifier.</summary>
        public long AuthorId { get; set; }

        /// <summary>Gets or sets the post title.</summary>
        public string Title { get; set; }

        /// <summary>Gets or sets the URL-friendly slug generated from the title.</summary>
        public string Slug { get; set; }

        /// <summary>Gets or sets the post body content.</summary>
        public string Content { get; set; }

        /// <summary>Gets or sets the full name of the author.</summary>
        public string AuthorName { get; set; }

        /// <summary>Gets or sets a value indicating whether the post is published.</summary>
        public bool Published { get; set; }

        /// <summary>Gets or sets the date and time the post was created.</summary>
        public DateTime? CreatedAt { get; set; }
    }
}
