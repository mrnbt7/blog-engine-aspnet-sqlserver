using System.Text.RegularExpressions;

namespace NbtBlogEngine.Helpers
{
    /// <summary>
    /// Generates URL-friendly slugs from text strings.
    /// </summary>
    public static class SlugHelper
    {
        /// <summary>
        /// Generates a URL-friendly slug from the given title.
        /// Converts to lowercase, removes special characters, and replaces spaces with hyphens.
        /// </summary>
        /// <param name="title">The title to convert into a slug.</param>
        /// <returns>A lowercase, hyphenated slug string, or <see cref="string.Empty"/> if the input is null or whitespace.</returns>
        public static string Generate(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return string.Empty;
            }

            string slug = title.ToLowerInvariant();
            slug = Regex.Replace(slug, @"[^a-z0-9\s-]", string.Empty);
            slug = Regex.Replace(slug, @"\s+", "-");
            slug = Regex.Replace(slug, @"-+", "-");
            return slug.Trim('-');
        }
    }
}
