using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NbtBlogEngine.Helpers;

namespace NbtBlogEngine.Tests
{
    [TestClass]
    public class TagHelperTests
    {
        [TestMethod]
        public void RenderBadges_EmptyList_ReturnsEmpty()
        {
            string result = TagHelper.RenderBadges(new List<string>());

            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void RenderBadges_SingleTag_ReturnsSingleBadge()
        {
            string result = TagHelper.RenderBadges(new List<string> { "C#" });

            Assert.IsTrue(result.Contains("tag=C%23"));
            Assert.IsTrue(result.Contains(">C#</a>"));
            Assert.IsTrue(result.Contains("badge"));
        }

        [TestMethod]
        public void RenderBadges_MultipleTags_ReturnsSpaceSeparated()
        {
            string result = TagHelper.RenderBadges(new List<string> { "AWS", "Cloud" });

            Assert.IsTrue(result.Contains("AWS"));
            Assert.IsTrue(result.Contains("Cloud"));
            Assert.IsTrue(result.Contains(" "));
        }

        [TestMethod]
        public void RenderBadges_CustomLinkPage_UsesCustomPage()
        {
            string result = TagHelper.RenderBadges(new List<string> { "Test" }, "Custom.aspx");

            Assert.IsTrue(result.Contains("Custom.aspx?tag="));
        }

        [TestMethod]
        public void RenderBadges_DefaultLinkPage_UsesBlogPage()
        {
            string result = TagHelper.RenderBadges(new List<string> { "Test" });

            Assert.IsTrue(result.Contains("Blog.aspx?tag="));
        }
    }
}
