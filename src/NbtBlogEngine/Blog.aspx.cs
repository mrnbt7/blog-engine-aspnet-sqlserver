using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using NbtBlogEngine.Helpers;
using NbtBlogEngine.Services;

namespace NbtBlogEngine
{
    public partial class Blog : System.Web.UI.Page
    {
        private PostService _postService;
        private TagService _tagService;

        protected void Page_Load(object sender, EventArgs e)
        {
            _postService = ServiceFactory.CreatePostService();
            _tagService = ServiceFactory.CreateTagService();

            if (!IsPostBack)
            {
                BindBlogPosts();
            }
        }

        protected void lvBlogPosts_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                long postId = Convert.ToInt64(((DataRowView)e.Item.DataItem)["id"]);
                var litTags = (Literal)e.Item.FindControl("litTags");
                if (litTags != null)
                {
                    litTags.Text = TagHelper.RenderBadges(_tagService.GetTagTitlesForPost(postId));
                }
            }
        }

        protected void lvBlogPosts_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            dpBlog.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindBlogPosts();
        }

        protected void btnBlogSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtBlogSearch.Text.Trim();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                Response.Redirect("Blog.aspx?q=" + Server.UrlEncode(searchTerm));
            }
            else
            {
                Response.Redirect("Blog.aspx");
            }
        }

        protected string TruncateContent(string content, int maxLength)
        {
            return TextHelper.Truncate(content, maxLength);
        }

        private void BindBlogPosts()
        {
            string tag = Request.QueryString["tag"];
            string search = Request.QueryString["q"];

            DataTable data;

            if (!string.IsNullOrEmpty(tag))
            {
                blogTitle.InnerText = "Posts tagged: " + tag;
                lnkClearFilter.Visible = true;
                data = _postService.GetPublishedPostsByTag(tag);
            }
            else if (!string.IsNullOrEmpty(search))
            {
                blogTitle.InnerText = "Search: " + search;
                txtBlogSearch.Text = search;
                lnkClearFilter.Visible = true;
                data = _postService.SearchPosts(search);
            }
            else
            {
                data = _postService.GetPublishedPosts();
            }

            lvBlogPosts.DataSource = data;
            lvBlogPosts.DataBind();
        }
    }
}
