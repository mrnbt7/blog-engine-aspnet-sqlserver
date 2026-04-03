using System.Collections.Generic;
using System.Data;

namespace NbtBlogEngine.Repositories
{
    /// <summary>
    /// Defines data access operations for tags and post-tag relationships.
    /// </summary>
    public interface ITagRepository
    {
        /// <summary>Gets all tags ordered by title.</summary>
        /// <returns>A <see cref="DataTable"/> containing all tags.</returns>
        DataTable GetAll();

        /// <summary>Gets the tag IDs associated with a post.</summary>
        /// <param name="postId">The post's unique identifier.</param>
        /// <returns>A list of tag IDs linked to the post.</returns>
        List<long> GetTagIdsForPost(long postId);

        /// <summary>Gets the tag titles associated with a post.</summary>
        /// <param name="postId">The post's unique identifier.</param>
        /// <returns>A list of tag title strings linked to the post.</returns>
        List<string> GetTagTitlesForPost(long postId);

        /// <summary>Replaces all tags for a post with the specified tag IDs.</summary>
        /// <param name="postId">The post's unique identifier.</param>
        /// <param name="tagIds">The list of tag IDs to associate with the post.</param>
        void SavePostTags(long postId, List<long> tagIds);

        /// <summary>Creates a new tag.</summary>
        /// <param name="title">The tag title.</param>
        /// <returns>The unique identifier of the newly created tag.</returns>
        long Create(string title);

        /// <summary>Deletes a tag if it is not used by any post.</summary>
        /// <param name="tagId">The tag's unique identifier.</param>
        /// <returns><c>true</c> if the tag was deleted; <c>false</c> if it is in use.</returns>
        bool Delete(long tagId);
    }
}
