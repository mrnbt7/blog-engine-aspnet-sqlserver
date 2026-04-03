using NbtBlogEngine.Models;

namespace NbtBlogEngine.Repositories
{
    /// <summary>
    /// Defines data access operations for users.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>Finds a user by email and password credentials.</summary>
        UserDTO FindByCredentials(string email, string password);

        /// <summary>Finds a user by ID with profile details and post count.</summary>
        UserDTO FindById(long userId);

        /// <summary>Creates a new user account.</summary>
        bool Create(string firstName, string lastName, string email, string password);

        /// <summary>Updates a user's profile fields.</summary>
        bool Update(long userId, string firstName, string lastName, string email);
    }
}
