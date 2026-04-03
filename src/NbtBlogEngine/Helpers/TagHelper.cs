using System.Collections.Generic;
using System.Globalization;
using System.Web;

namespace NbtBlogEngine.Helpers
{
    /// <summary>
    /// Renders tag badges as HTML anchor elements for display in the UI.
    /// </summary>
    public static class TagHelper
    {
        /// <summary>
        /// Renders a list of tag titles as Bootstrap badge links.
        /// </summary>
        /// <param name="tagTitles">The list of tag titles to render.</param>
        /// <param name="linkPage">The page to link each tag badge to. Defaults to "Blog.aspx".</param>
        /// <returns>A space-separated string of HTML anchor badge elements, or <see cref="string.Empty"/> if the list is empty.</returns>
        public static string RenderBadges(List<string> tagTitles, string linkPage = "Blog.aspx")
        {
            var badges = new List<string>();
            foreach (var tag in tagTitles)
            {
                badges.Add(string.Format(
                    CultureInfo.InvariantCulture,
                    "<a href='{0}?tag={1}' class='badge bg-secondary text-decoration-none me-1'>{2}</a>",
                    linkPage,
                    HttpUtility.UrlEncode(tag),
                    HttpUtility.HtmlEncode(tag)));
            }

            return string.Join(" ", badges);
        }
    }
}
