using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NbtBlogEngine.Models;
using NbtBlogEngine.Services;
using NbtBlogEngine.Tests.Mocks;

namespace NbtBlogEngine.Tests
{
    [TestClass]
    public class CommentServiceTests
    {
        private MockCommentRepository _mockRepo;
        private CommentService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = new MockCommentRepository();
            _service = new CommentService(_mockRepo);
        }

        [TestMethod]
        public void AddComment_ReturnsNewCommentId()
        {
            long commentId = _service.AddComment(1, 2, "Great post!");

            Assert.IsTrue(commentId > 0);
        }

        [TestMethod]
        public void AddComment_StoresCommentInRepository()
        {
            _service.AddComment(1, 2, "Nice article");

            Assert.AreEqual(1, _mockRepo.Comments.Count);
            Assert.AreEqual("Nice article", _mockRepo.Comments[0].Content);
            Assert.AreEqual(1, _mockRepo.Comments[0].PostId);
            Assert.AreEqual(2, _mockRepo.Comments[0].AuthorId);
        }

        [TestMethod]
        public void AddComment_SetsAuthorName()
        {
            _service.AddComment(5, 3, "Test");

            Assert.AreEqual("User 3", _mockRepo.Comments[0].AuthorName);
        }

        [TestMethod]
        public void GetCommentsByPostId_ReturnsCommentsForPost()
        {
            _mockRepo.Comments.Add(new CommentDTO { Id = 1, PostId = 10, AuthorId = 1, AuthorName = "Ravi", Content = "First", CreatedAt = DateTime.Now });
            _mockRepo.Comments.Add(new CommentDTO { Id = 2, PostId = 10, AuthorId = 2, AuthorName = "Priya", Content = "Second", CreatedAt = DateTime.Now });
            _mockRepo.Comments.Add(new CommentDTO { Id = 3, PostId = 20, AuthorId = 1, AuthorName = "Ravi", Content = "Other post", CreatedAt = DateTime.Now });

            List<CommentDTO> result = _service.GetCommentsByPostId(10);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("First", result[0].Content);
            Assert.AreEqual("Second", result[1].Content);
        }

        [TestMethod]
        public void GetCommentsByPostId_NoComments_ReturnsEmptyList()
        {
            List<CommentDTO> result = _service.GetCommentsByPostId(999);

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetCommentsByPostId_ReturnsAuthorNames()
        {
            _mockRepo.Comments.Add(new CommentDTO { Id = 1, PostId = 5, AuthorId = 1, AuthorName = "Ravi Kumar", Content = "Test", CreatedAt = DateTime.Now });

            List<CommentDTO> result = _service.GetCommentsByPostId(5);

            Assert.AreEqual("Ravi Kumar", result[0].AuthorName);
        }

        [TestMethod]
        public void GetCommentCount_ReturnsCorrectCount()
        {
            _mockRepo.Comments.Add(new CommentDTO { Id = 1, PostId = 10, Content = "A", CreatedAt = DateTime.Now });
            _mockRepo.Comments.Add(new CommentDTO { Id = 2, PostId = 10, Content = "B", CreatedAt = DateTime.Now });
            _mockRepo.Comments.Add(new CommentDTO { Id = 3, PostId = 20, Content = "C", CreatedAt = DateTime.Now });

            int count = _service.GetCommentCount(10);

            Assert.AreEqual(2, count);
        }

        [TestMethod]
        public void GetCommentCount_NoComments_ReturnsZero()
        {
            int count = _service.GetCommentCount(999);

            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public void GetCommentsByPostId_OrderedByCreatedAt()
        {
            _mockRepo.Comments.Add(new CommentDTO { Id = 1, PostId = 10, Content = "Later", CreatedAt = new DateTime(2024, 6, 2) });
            _mockRepo.Comments.Add(new CommentDTO { Id = 2, PostId = 10, Content = "Earlier", CreatedAt = new DateTime(2024, 6, 1) });

            List<CommentDTO> result = _service.GetCommentsByPostId(10);

            Assert.AreEqual("Earlier", result[0].Content);
            Assert.AreEqual("Later", result[1].Content);
        }

        [TestMethod]
        public void AddComment_MultipleComments_AllStored()
        {
            _service.AddComment(1, 1, "First");
            _service.AddComment(1, 2, "Second");
            _service.AddComment(1, 3, "Third");

            Assert.AreEqual(3, _mockRepo.Comments.Count);
            Assert.AreEqual(3, _service.GetCommentCount(1));
        }
    }
}
