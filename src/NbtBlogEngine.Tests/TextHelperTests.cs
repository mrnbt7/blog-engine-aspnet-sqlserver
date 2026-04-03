using Microsoft.VisualStudio.TestTools.UnitTesting;
using NbtBlogEngine.Helpers;

namespace NbtBlogEngine.Tests
{
    [TestClass]
    public class TextHelperTests
    {
        [TestMethod]
        public void Truncate_ShortText_ReturnsOriginal()
        {
            Assert.AreEqual("Hello", TextHelper.Truncate("Hello", 10));
        }

        [TestMethod]
        public void Truncate_ExactLength_ReturnsOriginal()
        {
            Assert.AreEqual("Hello", TextHelper.Truncate("Hello", 5));
        }

        [TestMethod]
        public void Truncate_LongText_TruncatesWithEllipsis()
        {
            Assert.AreEqual("Hel...", TextHelper.Truncate("Hello World", 3));
        }

        [TestMethod]
        public void Truncate_NullInput_ReturnsNull()
        {
            Assert.IsNull(TextHelper.Truncate(null, 10));
        }

        [TestMethod]
        public void Truncate_EmptyString_ReturnsEmpty()
        {
            Assert.AreEqual(string.Empty, TextHelper.Truncate(string.Empty, 10));
        }

        [TestMethod]
        public void Truncate_ZeroMaxLength_ReturnsEllipsisOnly()
        {
            Assert.AreEqual("...", TextHelper.Truncate("Hello", 0));
        }
    }
}
