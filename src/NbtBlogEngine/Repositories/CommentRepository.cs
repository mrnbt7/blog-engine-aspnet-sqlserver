using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using NbtBlogEngine.DataLayer;
using NbtBlogEngine.Models;

namespace NbtBlogEngine.Repositories
{
    /// <summary>
    /// SQL Server implementation of <see cref="ICommentRepository"/>.
    /// </summary>
    public class CommentRepository : ICommentRepository
    {
        private readonly ISqlHelper _db;

        /// <summary>Initializes a new instance of the <see cref="CommentRepository"/> class.</summary>
        /// <param name="db">The SQL helper for database access.</param>
        public CommentRepository(ISqlHelper db)
        {
            _db = db;
        }

        /// <inheritdoc/>
        public List<CommentDTO> GetByPostId(long postId)
        {
            var comments = new List<CommentDTO>();
            using (var reader = _db.ExecuteReader(
                SqlQueries.Comment.SelectByPostId,
                CommandType.Text,
                new SqlParameter("@PostId", SqlDbType.BigInt) { Value = postId }))
            {
                while (reader.Read())
                {
                    comments.Add(new CommentDTO
                    {
                        Id = Convert.ToInt64(reader["id"]),
                        PostId = Convert.ToInt64(reader["postId"]),
                        AuthorId = Convert.ToInt64(reader["authorId"]),
                        AuthorName = reader["authorName"]?.ToString()?.Trim(),
                        Content = reader["content"]?.ToString(),
                        CreatedAt = Convert.ToDateTime(reader["createdAt"])
                    });
                }
            }

            return comments;
        }

        /// <inheritdoc/>
        public long Create(long postId, long authorId, string content)
        {
            return Convert.ToInt64(_db.ExecuteScalar(
                SqlQueries.Comment.Create,
                CommandType.Text,
                new SqlParameter("@PostId", SqlDbType.BigInt) { Value = postId },
                new SqlParameter("@AuthorId", SqlDbType.BigInt) { Value = authorId },
                new SqlParameter("@Content", SqlDbType.NVarChar) { Value = content }));
        }

        /// <inheritdoc/>
        public int GetCountByPostId(long postId)
        {
            return Convert.ToInt32(_db.ExecuteScalar(
                SqlQueries.Comment.CountByPostId,
                CommandType.Text,
                new SqlParameter("@PostId", SqlDbType.BigInt) { Value = postId }));
        }
    }
}
