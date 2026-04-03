using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NbtBlogEngine.Services;
using NbtBlogEngine.Tests.Mocks;

namespace NbtBlogEngine.Tests
{
    [TestClass]
    public class TagServiceTests
    {
        private MockTagRepository _mockRepo;
        private TagService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = new MockTagRepository();
            _mockRepo.Tags[1] = "C#";
            _mockRepo.Tags[2] = "ASP.NET";
            _mockRepo.Tags[3] = "SQL Server";
            _service = new TagService(_mockRepo);
        }

        [TestMethod]
        public void GetAll_ReturnsAllTags()
        {
            var result = _service.GetAll();

            Assert.AreEqual(3, result.Rows.Count);
        }

        [TestMethod]
        public void CreateTag_ReturnsNewId()
        {
            long tagId = _service.CreateTag("JavaScript");

            Assert.IsTrue(tagId > 0);
            Assert.AreEqual(4, _mockRepo.Tags.Count);
        }

        [TestMethod]
        public void DeleteTag_UnusedTag_ReturnsTrue()
        {
            bool result = _service.DeleteTag(3);

            Assert.IsTrue(result);
            Assert.AreEqual(2, _mockRepo.Tags.Count);
        }

        [TestMethod]
        public void DeleteTag_UsedByPost_ReturnsFalse()
        {
            _mockRepo.PostTags[10] = new List<long> { 1 };

            bool result = _service.DeleteTag(1);

            Assert.IsFalse(result);
            Assert.AreEqual(3, _mockRepo.Tags.Count);
        }

        [TestMethod]
        public void SavePostTags_StoresTagIds()
        {
            var tagIds = new List<long> { 1, 2 };

            _service.SavePostTags(10, tagIds);

            Assert.AreEqual(2, _mockRepo.PostTags[10].Count);
            CollectionAssert.Contains(_mockRepo.PostTags[10], 1L);
            CollectionAssert.Contains(_mockRepo.PostTags[10], 2L);
        }

        [TestMethod]
        public void SavePostTags_ReplacesExistingTags()
        {
            _mockRepo.PostTags[10] = new List<long> { 1, 2, 3 };

            _service.SavePostTags(10, new List<long> { 2 });

            Assert.AreEqual(1, _mockRepo.PostTags[10].Count);
            CollectionAssert.Contains(_mockRepo.PostTags[10], 2L);
        }

        [TestMethod]
        public void GetTagIdsForPost_ReturnsCorrectIds()
        {
            _mockRepo.PostTags[5] = new List<long> { 1, 3 };

            List<long> result = _service.GetTagIdsForPost(5);

            Assert.AreEqual(2, result.Count);
            CollectionAssert.Contains(result, 1L);
            CollectionAssert.Contains(result, 3L);
        }

        [TestMethod]
        public void GetTagIdsForPost_NoTags_ReturnsEmptyList()
        {
            List<long> result = _service.GetTagIdsForPost(999);

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetTagTitlesForPost_ReturnsTitles()
        {
            _mockRepo.PostTags[5] = new List<long> { 1, 2 };

            List<string> result = _service.GetTagTitlesForPost(5);

            Assert.AreEqual(2, result.Count);
            CollectionAssert.Contains(result, "C#");
            CollectionAssert.Contains(result, "ASP.NET");
        }
    }
}
