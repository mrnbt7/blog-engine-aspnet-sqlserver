using System.Collections.Generic;
using System.Data;
using System.Linq;
using NbtBlogEngine.Repositories;

namespace NbtBlogEngine.Tests.Mocks
{
    public class MockTagRepository : ITagRepository
    {
        public Dictionary<long, string> Tags { get; set; } = new Dictionary<long, string>();
        public Dictionary<long, List<long>> PostTags { get; set; } = new Dictionary<long, List<long>>();
        public long NextId { get; set; } = 100;
        public bool DeleteCalled { get; set; }

        public DataTable GetAll()
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("id", typeof(long));
            dataTable.Columns.Add("title", typeof(string));
            foreach (var tag in Tags)
            {
                dataTable.Rows.Add(tag.Key, tag.Value);
            }

            return dataTable;
        }

        public List<long> GetTagIdsForPost(long postId)
        {
            return PostTags.ContainsKey(postId) ? PostTags[postId] : new List<long>();
        }

        public List<string> GetTagTitlesForPost(long postId)
        {
            var tagIds = GetTagIdsForPost(postId);
            return tagIds.Where(id => Tags.ContainsKey(id)).Select(id => Tags[id]).ToList();
        }

        public void SavePostTags(long postId, List<long> tagIds)
        {
            PostTags[postId] = new List<long>(tagIds);
        }

        public long Create(string title)
        {
            NextId++;
            Tags[NextId] = title;
            return NextId;
        }

        public bool Delete(long tagId)
        {
            DeleteCalled = true;
            foreach (var postTag in PostTags.Values)
            {
                if (postTag.Contains(tagId))
                {
                    return false;
                }
            }

            return Tags.Remove(tagId);
        }
    }
}
