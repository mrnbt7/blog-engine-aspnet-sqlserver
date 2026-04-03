using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using NbtBlogEngine.Helpers;
using NbtBlogEngine.Services;

namespace NbtBlogEngine
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPosts();
            }
        }

        protected void lvBlogPosts_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            dpHome.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindPosts();
        }

        protected string TruncateContent(string content, int maxLength)
        {
            return TextHelper.Truncate(content, maxLength);
        }

        private void BindPosts()
        {
            lvBlogPosts.DataSource = ServiceFactory.CreatePostService().GetPublishedPosts();
            lvBlogPosts.DataBind();
        }
    }
}
