using System;
using System.Data;
using System.Data.SqlClient;
using NbtBlogEngine.DataLayer;
using NbtBlogEngine.Models;

namespace NbtBlogEngine.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ISqlHelper _db;

        public UserRepository(ISqlHelper db)
        {
            _db = db;
        }

        public UserDTO FindByCredentials(string email, string password)
        {
            using (var reader = _db.ExecuteReader(
                SqlQueries.User.FindByCredentials,
                CommandType.Text,
                new SqlParameter("@Email", SqlDbType.NVarChar, 50) { Value = email },
                new SqlParameter("@Password", SqlDbType.NVarChar, 32) { Value = password }))
            {
                if (reader.Read())
                {
                    return new UserDTO
                    {
                        Id = Convert.ToInt64(reader["id"]),
                        FirstName = reader["firstName"]?.ToString(),
                        Email = reader["email"]?.ToString()
                    };
                }
            }

            return null;
        }

        public UserDTO FindById(long userId)
        {
            using (var reader = _db.ExecuteReader(
                SqlQueries.User.FindById,
                CommandType.Text,
                new SqlParameter("@UserId", SqlDbType.BigInt) { Value = userId }))
            {
                if (reader.Read())
                {
                    return new UserDTO
                    {
                        Id = Convert.ToInt64(reader["id"]),
                        FirstName = reader["firstName"]?.ToString(),
                        LastName = reader["lastName"]?.ToString(),
                        Email = reader["email"]?.ToString(),
                        RegisteredAt = reader["registeredAt"] != DBNull.Value ? Convert.ToDateTime(reader["registeredAt"]) : (DateTime?)null,
                        LastLogin = reader["lastLogin"] != DBNull.Value ? Convert.ToDateTime(reader["lastLogin"]) : (DateTime?)null,
                        PostCount = Convert.ToInt32(reader["postCount"])
                    };
                }
            }

            return null;
        }

        public bool Create(string firstName, string lastName, string email, string password)
        {
            return _db.ExecuteNonQuery(
                SqlQueries.User.Create,
                CommandType.Text,
                new SqlParameter("@FirstName", SqlDbType.NVarChar, 50) { Value = firstName },
                new SqlParameter("@LastName", SqlDbType.NVarChar, 50) { Value = (object)lastName ?? DBNull.Value },
                new SqlParameter("@Email", SqlDbType.NVarChar, 50) { Value = email },
                new SqlParameter("@Password", SqlDbType.NVarChar, 32) { Value = password }) > 0;
        }

        public bool Update(long userId, string firstName, string lastName, string email)
        {
            return _db.ExecuteNonQuery(
                SqlQueries.User.Update,
                CommandType.Text,
                new SqlParameter("@UserId", SqlDbType.BigInt) { Value = userId },
                new SqlParameter("@FirstName", SqlDbType.NVarChar, 50) { Value = firstName },
                new SqlParameter("@LastName", SqlDbType.NVarChar, 50) { Value = (object)lastName ?? DBNull.Value },
                new SqlParameter("@Email", SqlDbType.NVarChar, 50) { Value = email }) > 0;
        }
    }
}
