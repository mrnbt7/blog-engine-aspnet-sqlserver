using Microsoft.VisualStudio.TestTools.UnitTesting;
using NbtBlogEngine.Models;
using NbtBlogEngine.Services;
using NbtBlogEngine.Tests.Mocks;

namespace NbtBlogEngine.Tests
{
    [TestClass]
    public class PostServiceTests
    {
        private MockPostRepository _mockRepo;
        private PostService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = new MockPostRepository();
            _service = new PostService(_mockRepo);
        }

        [TestMethod]
        public void CreatePost_GeneratesSlugFromTitle()
        {
            _service.CreatePost(1, "Hello World", "content", true);

            Assert.AreEqual("hello-world", _mockRepo.LastSlugPassed);
        }

        [TestMethod]
        public void CreatePost_ReturnsNewPostId()
        {
            long postId = _service.CreatePost(1, "Test Post", "content", false);

            Assert.IsTrue(postId > 0);
        }

        [TestMethod]
        public void CreatePost_AddsPostToRepository()
        {
            _service.CreatePost(1, "Test", "body", true);

            Assert.AreEqual(1, _mockRepo.Posts.Count);
            Assert.AreEqual("Test", _mockRepo.Posts[0].Title);
        }

        [TestMethod]
        public void UpdatePost_GeneratesSlugFromTitle()
        {
            _mockRepo.Posts.Add(new PostDTO { Id = 1, AuthorId = 1, Title = "Old", Slug = "old" });

            _service.UpdatePost(1, 1, "New Title", "content", true);

            Assert.AreEqual("new-title", _mockRepo.LastSlugPassed);
        }

        [TestMethod]
        public void UpdatePost_NonExistentPost_ReturnsFalse()
        {
            bool result = _service.UpdatePost(999, 1, "Title", "content", true);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetPostById_ExistingPost_ReturnsPost()
        {
            _mockRepo.Posts.Add(new PostDTO { Id = 5, Title = "Found" });

            PostDTO result = _service.GetPostById(5);

            Assert.IsNotNull(result);
            Assert.AreEqual("Found", result.Title);
        }

        [TestMethod]
        public void GetPostById_NonExistentPost_ReturnsNull()
        {
            PostDTO result = _service.GetPostById(999);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetPostBySlug_ExistingSlug_ReturnsPost()
        {
            _mockRepo.Posts.Add(new PostDTO { Id = 1, Slug = "test-slug", Title = "Test" });

            PostDTO result = _service.GetPostBySlug("test-slug");

            Assert.IsNotNull(result);
            Assert.AreEqual("Test", result.Title);
        }

        [TestMethod]
        public void GetPostBySlug_NonExistentSlug_ReturnsNull()
        {
            PostDTO result = _service.GetPostBySlug("no-such-slug");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void DeletePost_ExistingPost_ReturnsTrue()
        {
            _mockRepo.Posts.Add(new PostDTO { Id = 1 });

            bool result = _service.DeletePost(1);

            Assert.IsTrue(result);
            Assert.AreEqual(0, _mockRepo.Posts.Count);
        }

        [TestMethod]
        public void DeletePost_NonExistentPost_ReturnsFalse()
        {
            bool result = _service.DeletePost(999);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetPublishedPosts_ReturnsOnlyPublished()
        {
            _mockRepo.Posts.Add(new PostDTO { Id = 1, Published = true, Title = "Pub" });
            _mockRepo.Posts.Add(new PostDTO { Id = 2, Published = false, Title = "Draft" });

            var result = _service.GetPublishedPosts();

            Assert.AreEqual(1, result.Rows.Count);
        }

        [TestMethod]
        public void GetPostsByAuthor_FiltersCorrectly()
        {
            _mockRepo.Posts.Add(new PostDTO { Id = 1, AuthorId = 1, Title = "Mine" });
            _mockRepo.Posts.Add(new PostDTO { Id = 2, AuthorId = 2, Title = "Theirs" });

            var result = _service.GetPostsByAuthor(1);

            Assert.AreEqual(1, result.Rows.Count);
        }
    }
}
