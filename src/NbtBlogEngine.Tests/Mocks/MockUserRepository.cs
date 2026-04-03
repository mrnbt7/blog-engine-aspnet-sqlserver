using System.Collections.Generic;
using NbtBlogEngine.Models;
using NbtBlogEngine.Repositories;

namespace NbtBlogEngine.Tests.Mocks
{
    public class MockUserRepository : IUserRepository
    {
        public List<UserDTO> Users { get; set; } = new List<UserDTO>();
        public bool CreateCalled { get; set; }
        public bool UpdateCalled { get; set; }
        public string LastEmailRegistered { get; set; }

        public UserDTO FindByCredentials(string email, string password)
        {
            return Users.Find(u => u.Email == email);
        }

        public UserDTO FindById(long userId)
        {
            return Users.Find(u => u.Id == userId);
        }

        public bool Create(string firstName, string lastName, string email, string password)
        {
            CreateCalled = true;
            LastEmailRegistered = email;
            Users.Add(new UserDTO { Id = Users.Count + 1, FirstName = firstName, LastName = lastName, Email = email });
            return true;
        }

        public bool Update(long userId, string firstName, string lastName, string email)
        {
            UpdateCalled = true;
            var user = Users.Find(u => u.Id == userId);
            if (user == null)
            {
                return false;
            }

            user.FirstName = firstName;
            user.LastName = lastName;
            user.Email = email;
            return true;
        }
    }
}
