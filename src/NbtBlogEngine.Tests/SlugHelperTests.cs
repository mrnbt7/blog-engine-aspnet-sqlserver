using Microsoft.VisualStudio.TestTools.UnitTesting;
using NbtBlogEngine.Helpers;

namespace NbtBlogEngine.Tests
{
    [TestClass]
    public class SlugHelperTests
    {
        [TestMethod]
        public void Generate_SimpleTitle_ReturnsLowercaseHyphenated()
        {
            Assert.AreEqual("hello-world", SlugHelper.Generate("Hello World"));
        }

        [TestMethod]
        public void Generate_TitleWithSpecialChars_RemovesSpecialChars()
        {
            Assert.AreEqual("getting-started-with-c-12", SlugHelper.Generate("Getting Started with C# 12"));
        }

        [TestMethod]
        public void Generate_TitleWithMultipleSpaces_SingleHyphen()
        {
            Assert.AreEqual("hello-world", SlugHelper.Generate("Hello   World"));
        }

        [TestMethod]
        public void Generate_TitleWithLeadingTrailingSpaces_TrimmedSlug()
        {
            Assert.AreEqual("hello", SlugHelper.Generate("  Hello  "));
        }

        [TestMethod]
        public void Generate_NullInput_ReturnsEmpty()
        {
            Assert.AreEqual(string.Empty, SlugHelper.Generate(null));
        }

        [TestMethod]
        public void Generate_EmptyString_ReturnsEmpty()
        {
            Assert.AreEqual(string.Empty, SlugHelper.Generate(string.Empty));
        }

        [TestMethod]
        public void Generate_WhitespaceOnly_ReturnsEmpty()
        {
            Assert.AreEqual(string.Empty, SlugHelper.Generate("   "));
        }

        [TestMethod]
        public void Generate_TitleWithNumbers_KeepsNumbers()
        {
            Assert.AreEqual("top-10-tips", SlugHelper.Generate("Top 10 Tips"));
        }

        [TestMethod]
        public void Generate_TitleWithHyphens_PreservesHyphens()
        {
            Assert.AreEqual("asp-net-core", SlugHelper.Generate("ASP-NET Core"));
        }

        [TestMethod]
        public void Generate_TitleWithConsecutiveHyphens_CollapsesToSingle()
        {
            Assert.AreEqual("hello-world", SlugHelper.Generate("Hello---World"));
        }
    }
}
