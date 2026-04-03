using NbtBlogEngine.Models;
using NbtBlogEngine.Repositories;

namespace NbtBlogEngine.Services
{
    /// <summary>
    /// Provides business logic for user authentication, registration, and profile management.
    /// </summary>
    public class UserService
    {
        private readonly IUserRepository _repo;

        /// <summary>Initializes a new instance of the <see cref="UserService"/> class.</summary>
        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        /// <summary>Validates user credentials and returns the user if valid.</summary>
        public UserDTO ValidateUser(string email, string password) => _repo.FindByCredentials(email, password);

        /// <summary>Gets a user's profile by ID.</summary>
        public UserDTO GetProfile(long userId) => _repo.FindById(userId);

        /// <summary>Updates a user's profile.</summary>
        public bool UpdateProfile(long userId, string firstName, string lastName, string email)
        {
            return _repo.Update(userId, firstName, lastName, email);
        }

        /// <summary>Registers a new user account.</summary>
        public bool Register(string firstName, string lastName, string email, string password)
        {
            return _repo.Create(firstName, lastName, email, password);
        }
    }
}
