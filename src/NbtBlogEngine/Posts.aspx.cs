using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using NbtBlogEngine.Services;

namespace NbtBlogEngine
{
    public partial class PostList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPosts();
            }
        }

        protected void btnNewPost_Click(object sender, EventArgs e)
        {
            Response.Redirect("NewPost.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            dpPosts.SetPageProperties(0, dpPosts.MaximumRows, false);
            BindPosts(txtSearch.Text.Trim());
        }

        protected void lvBlogPosts_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            dpPosts.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindPosts(txtSearch.Text.Trim());
        }

        private void BindPosts(string searchTerm = null)
        {
            var service = ServiceFactory.CreatePostService();

            lvBlogPosts.DataSource = string.IsNullOrWhiteSpace(searchTerm)
                ? service.GetPostsByAuthor(GetLoggedInUserId())
                : service.SearchPosts(searchTerm);
            lvBlogPosts.DataBind();
        }

        private int GetLoggedInUserId()
        {
            if (Session["UserId"] != null)
            {
                return Convert.ToInt32(Session["UserId"]);
            }

            Response.Redirect("~/Login");
            return 0;
        }
    }
}
