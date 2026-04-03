using Microsoft.VisualStudio.TestTools.UnitTesting;
using NbtBlogEngine.Models;
using NbtBlogEngine.Services;
using NbtBlogEngine.Tests.Mocks;

namespace NbtBlogEngine.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        private MockUserRepository _mockRepo;
        private UserService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = new MockUserRepository();
            _service = new UserService(_mockRepo);
        }

        [TestMethod]
        public void ValidateUser_ValidCredentials_ReturnsUser()
        {
            _mockRepo.Users.Add(new UserDTO { Id = 1, FirstName = "Ravi", Email = "ravi@test.com" });

            UserDTO result = _service.ValidateUser("ravi@test.com", "anypassword");

            Assert.IsNotNull(result);
            Assert.AreEqual("Ravi", result.FirstName);
        }

        [TestMethod]
        public void ValidateUser_InvalidEmail_ReturnsNull()
        {
            UserDTO result = _service.ValidateUser("nobody@test.com", "pass");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Register_NewUser_ReturnsTrue()
        {
            bool result = _service.Register("Priya", "Sharma", "priya@test.com", "pass123");

            Assert.IsTrue(result);
            Assert.IsTrue(_mockRepo.CreateCalled);
            Assert.AreEqual("priya@test.com", _mockRepo.LastEmailRegistered);
        }

        [TestMethod]
        public void Register_AddsUserToRepository()
        {
            _service.Register("Arjun", "Reddy", "arjun@test.com", "pass");

            Assert.AreEqual(1, _mockRepo.Users.Count);
            Assert.AreEqual("Arjun", _mockRepo.Users[0].FirstName);
        }

        [TestMethod]
        public void GetProfile_ExistingUser_ReturnsUser()
        {
            _mockRepo.Users.Add(new UserDTO { Id = 5, FirstName = "Ravi", LastName = "Kumar", Email = "ravi@test.com" });

            UserDTO result = _service.GetProfile(5);

            Assert.IsNotNull(result);
            Assert.AreEqual("Ravi", result.FirstName);
            Assert.AreEqual("Kumar", result.LastName);
        }

        [TestMethod]
        public void GetProfile_NonExistentUser_ReturnsNull()
        {
            UserDTO result = _service.GetProfile(999);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void UpdateProfile_ExistingUser_ReturnsTrue()
        {
            _mockRepo.Users.Add(new UserDTO { Id = 1, FirstName = "Old", LastName = "Name", Email = "old@test.com" });

            bool result = _service.UpdateProfile(1, "New", "Name", "new@test.com");

            Assert.IsTrue(result);
            Assert.IsTrue(_mockRepo.UpdateCalled);
            Assert.AreEqual("New", _mockRepo.Users[0].FirstName);
            Assert.AreEqual("new@test.com", _mockRepo.Users[0].Email);
        }

        [TestMethod]
        public void UpdateProfile_NonExistentUser_ReturnsFalse()
        {
            bool result = _service.UpdateProfile(999, "Name", "Last", "email@test.com");

            Assert.IsFalse(result);
        }
    }
}
