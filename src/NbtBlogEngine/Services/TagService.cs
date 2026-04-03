using System.Collections.Generic;
using System.Data;
using NbtBlogEngine.Repositories;

namespace NbtBlogEngine.Services
{
    /// <summary>
    /// Provides business logic for tag management and post-tag relationships.
    /// </summary>
    public class TagService
    {
        private readonly ITagRepository _repo;

        /// <summary>Initializes a new instance of the <see cref="TagService"/> class.</summary>
        /// <param name="repo">The tag repository for data access.</param>
        public TagService(ITagRepository repo)
        {
            _repo = repo;
        }

        /// <summary>Gets all tags.</summary>
        /// <returns>A <see cref="DataTable"/> containing all tags.</returns>
        public DataTable GetAll() => _repo.GetAll();

        /// <summary>Gets the tag IDs associated with a post.</summary>
        /// <param name="postId">The post's unique identifier.</param>
        /// <returns>A list of tag IDs.</returns>
        public List<long> GetTagIdsForPost(long postId) => _repo.GetTagIdsForPost(postId);

        /// <summary>Gets the tag titles associated with a post.</summary>
        /// <param name="postId">The post's unique identifier.</param>
        /// <returns>A list of tag title strings.</returns>
        public List<string> GetTagTitlesForPost(long postId) => _repo.GetTagTitlesForPost(postId);

        /// <summary>Replaces all tags for a post with the specified tag IDs.</summary>
        /// <param name="postId">The post's unique identifier.</param>
        /// <param name="tagIds">The tag IDs to associate with the post.</param>
        public void SavePostTags(long postId, List<long> tagIds) => _repo.SavePostTags(postId, tagIds);

        /// <summary>Creates a new tag.</summary>
        /// <param name="title">The tag title.</param>
        /// <returns>The unique identifier of the newly created tag.</returns>
        public long CreateTag(string title) => _repo.Create(title);

        /// <summary>Deletes a tag if it is not used by any post.</summary>
        /// <param name="tagId">The tag's unique identifier.</param>
        /// <returns><c>true</c> if deleted; <c>false</c> if the tag is in use.</returns>
        public bool DeleteTag(long tagId) => _repo.Delete(tagId);
    }
}
