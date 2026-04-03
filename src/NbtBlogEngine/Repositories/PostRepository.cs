using System;
using System.Data;
using System.Data.SqlClient;
using NbtBlogEngine.DataLayer;
using NbtBlogEngine.Models;

namespace NbtBlogEngine.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ISqlHelper _db;

        public PostRepository(ISqlHelper db)
        {
            _db = db;
        }

        public DataTable GetByAuthor(int authorId)
        {
            return ToDataTable(_db.ExecuteReader(
                SqlQueries.Post.SelectByAuthor,
                CommandType.Text,
                new SqlParameter("@AuthorId", SqlDbType.BigInt) { Value = authorId }));
        }

        public long Create(int authorId, string title, string slug, string content, bool published)
        {
            var parameters = new[]
            {
                new SqlParameter("@AuthorId", authorId),
                new SqlParameter("@Title", title),
                new SqlParameter("@Slug", slug),
                new SqlParameter("@Content", NullIfEmpty(content)),
                new SqlParameter("@Published", published ? 1 : 0),
                new SqlParameter(published ? "@PublishedAt" : "@CreatedAt", DateTime.Now)
            };

            return Convert.ToInt64(_db.ExecuteScalar(SqlQueries.Post.CreateStoredProc, CommandType.StoredProcedure, parameters));
        }

        public bool Update(long postId, int authorId, string title, string slug, string content, bool published)
        {
            return _db.ExecuteNonQuery(
                SqlQueries.Post.Update,
                CommandType.Text,
                new SqlParameter("@PostId", SqlDbType.BigInt) { Value = postId },
                new SqlParameter("@AuthorId", SqlDbType.BigInt) { Value = authorId },
                new SqlParameter("@Title", SqlDbType.NVarChar, 75) { Value = title },
                new SqlParameter("@Slug", SqlDbType.NVarChar, 100) { Value = slug },
                new SqlParameter("@Content", SqlDbType.NVarChar) { Value = NullIfEmpty(content) },
                new SqlParameter("@Published", SqlDbType.Bit) { Value = published }) > 0;
        }

        public PostDTO FindById(long postId)
        {
            return FindSinglePost(
                SqlQueries.Post.FindById,
                new SqlParameter("@PostId", SqlDbType.BigInt) { Value = postId });
        }

        public PostDTO FindBySlug(string slug)
        {
            return FindSinglePost(
                SqlQueries.Post.FindBySlug,
                new SqlParameter("@Slug", SqlDbType.NVarChar, 100) { Value = slug });
        }

        public bool Delete(long postId)
        {
            return _db.ExecuteNonQuery(
                SqlQueries.Post.Delete,
                CommandType.Text,
                new SqlParameter("@PostId", SqlDbType.BigInt) { Value = postId }) > 0;
        }

        public DataTable GetPublished()
        {
            return ToDataTable(_db.ExecuteReader(SqlQueries.Post.SelectPublished, CommandType.Text));
        }

        public DataTable GetPublishedByTag(string tagTitle)
        {
            return ToDataTable(_db.ExecuteReader(
                SqlQueries.Post.SelectPublishedByTag,
                CommandType.Text,
                new SqlParameter("@TagTitle", SqlDbType.NVarChar, 75) { Value = tagTitle }));
        }

        public DataTable Search(string searchTerm)
        {
            return ToDataTable(_db.ExecuteReader(
                SqlQueries.Post.Search,
                CommandType.Text,
                new SqlParameter("@SearchTerm", SqlDbType.NVarChar) { Value = "%" + searchTerm + "%" }));
        }

        private static PostDTO MapReaderToPost(SqlDataReader reader)
        {
            return new PostDTO
            {
                Id = Convert.ToInt64(reader["id"]),
                AuthorId = Convert.ToInt64(reader["authorId"]),
                Title = reader["title"]?.ToString(),
                Slug = reader["slug"]?.ToString(),
                Content = reader["content"]?.ToString(),
                AuthorName = HasColumn(reader, "authorName") ? reader["authorName"]?.ToString()?.Trim() : null,
                Published = Convert.ToBoolean(reader["published"]),
                CreatedAt = reader["createdAt"] != DBNull.Value ? Convert.ToDateTime(reader["createdAt"]) : (DateTime?)null
            };
        }

        private static DataTable ToDataTable(SqlDataReader reader)
        {
            var dataTable = new DataTable();
            dataTable.Load(reader);
            reader.Close();
            return dataTable;
        }

        private static object NullIfEmpty(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? (object)DBNull.Value : value;
        }

        private static bool HasColumn(SqlDataReader reader, string columnName)
        {
            for (int fieldIndex = 0; fieldIndex < reader.FieldCount; fieldIndex++)
            {
                if (reader.GetName(fieldIndex).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private PostDTO FindSinglePost(string query, params SqlParameter[] parameters)
        {
            using (var reader = _db.ExecuteReader(query, CommandType.Text, parameters))
            {
                return reader.Read() ? MapReaderToPost(reader) : null;
            }
        }
    }
}
