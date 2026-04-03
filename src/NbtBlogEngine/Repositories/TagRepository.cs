using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using NbtBlogEngine.DataLayer;

namespace NbtBlogEngine.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly ISqlHelper _db;

        public TagRepository(ISqlHelper db)
        {
            _db = db;
        }

        public DataTable GetAll()
        {
            var tagsTable = new DataTable();
            using (var reader = _db.ExecuteReader(SqlQueries.Tag.SelectAll, CommandType.Text))
            {
                tagsTable.Load(reader);
            }

            return tagsTable;
        }

        public List<long> GetTagIdsForPost(long postId)
        {
            var tagIds = new List<long>();
            using (var reader = _db.ExecuteReader(
                SqlQueries.Tag.SelectIdsForPost,
                CommandType.Text,
                new SqlParameter("@PostId", SqlDbType.BigInt) { Value = postId }))
            {
                while (reader.Read())
                {
                    tagIds.Add(Convert.ToInt64(reader["tagId"]));
                }
            }

            return tagIds;
        }

        public List<string> GetTagTitlesForPost(long postId)
        {
            var tagTitles = new List<string>();
            using (var reader = _db.ExecuteReader(
                SqlQueries.Tag.SelectTitlesForPost,
                CommandType.Text,
                new SqlParameter("@PostId", SqlDbType.BigInt) { Value = postId }))
            {
                while (reader.Read())
                {
                    tagTitles.Add(reader["title"].ToString());
                }
            }

            return tagTitles;
        }

        public void SavePostTags(long postId, List<long> tagIds)
        {
            _db.ExecuteNonQuery(
                SqlQueries.Tag.DeletePostTags,
                CommandType.Text,
                new SqlParameter("@PostId", SqlDbType.BigInt) { Value = postId });

            foreach (var tagId in tagIds)
            {
                _db.ExecuteNonQuery(
                    SqlQueries.Tag.InsertPostTag,
                    CommandType.Text,
                    new SqlParameter("@PostId", SqlDbType.BigInt) { Value = postId },
                    new SqlParameter("@TagId", SqlDbType.BigInt) { Value = tagId });
            }
        }

        public long Create(string title)
        {
            return Convert.ToInt64(_db.ExecuteScalar(
                SqlQueries.Tag.Create,
                CommandType.Text,
                new SqlParameter("@Title", SqlDbType.NVarChar, 75) { Value = title }));
        }

        public bool Delete(long tagId)
        {
            return _db.ExecuteNonQuery(
                SqlQueries.Tag.Delete,
                CommandType.Text,
                new SqlParameter("@TagId", SqlDbType.BigInt) { Value = tagId }) > 0;
        }
    }
}
