namespace NbtBlogEngine.Helpers
{
    /// <summary>
    /// Provides utility methods for text manipulation.
    /// </summary>
    public static class TextHelper
    {
        /// <summary>
        /// Truncates the text to the specified maximum length and appends an ellipsis.
        /// </summary>
        /// <param name="text">The text to truncate.</param>
        /// <param name="maxLength">The maximum number of characters before truncation.</param>
        /// <returns>The original text if shorter than <paramref name="maxLength"/>, otherwise the truncated text with "..." appended.</returns>
        public static string Truncate(string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text) || text.Length <= maxLength)
            {
                return text;
            }

            return text.Substring(0, maxLength) + "...";
        }
    }
}
