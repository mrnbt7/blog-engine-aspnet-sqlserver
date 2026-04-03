using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using NbtBlogEngine.Services;

namespace NbtBlogEngine
{
    public partial class NewPost : System.Web.UI.Page
    {
        private PostService _postService;
        private TagService _tagService;
        private bool _isPublish;

        protected void Page_Load(object sender, EventArgs e)
        {
            _postService = ServiceFactory.CreatePostService();
            _tagService = ServiceFactory.CreateTagService();

            if (Session["UserId"] == null)
            {
                Response.Redirect("~/Login");
                return;
            }

            btnUpdate.Visible = _isPublish;
            btnPreview.Visible = _isPublish;
            btnDelete.Visible = _isPublish;

            if (!IsPostBack)
            {
                BindTags();
                if (long.TryParse(Request.QueryString["id"], out long postId))
                {
                    ViewState["PostId"] = postId;
                    btnDelete.Visible = true;
                    btnPreview.Visible = true;
                    LoadPost(postId);
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            GoToPostsPage();
        }

        protected void btnPublish_Click(object sender, EventArgs e)
        {
            if (!ValidatePost())
            {
                return;
            }

            SavePost(true);
            GoToPostsPage();
        }

        protected void btnSaveDraft_Click(object sender, EventArgs e)
        {
            if (!ValidatePost())
            {
                return;
            }

            SavePost(false);
            GoToPostsPage();
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            Response.Redirect("PostDetails.aspx?id=" + ViewState["PostId"]);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (ViewState["PostId"] != null)
            {
                long postId = Convert.ToInt64(ViewState["PostId"]);
                _tagService.SavePostTags(postId, new List<long>());
                _postService.DeletePost(postId);
            }

            GoToPostsPage();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!ValidatePost())
            {
                return;
            }

            long postId = Convert.ToInt64(ViewState["PostId"]);
            _postService.UpdatePost(postId, GetLoggedInUserId(), txtTitle.Text, txtContent.Text, _isPublish);
            _tagService.SavePostTags(postId, GetSelectedTagIds());
            GoToPostsPage();
        }

        protected void btnAddTag_Click(object sender, EventArgs e)
        {
            string tagName = txtNewTag.Text.Trim();
            if (string.IsNullOrWhiteSpace(tagName))
            {
                lblTagMessage.ForeColor = System.Drawing.Color.Red;
                lblTagMessage.Text = "Tag name is required.";
                return;
            }

            try
            {
                _tagService.CreateTag(tagName);
                txtNewTag.Text = string.Empty;
                lblTagMessage.ForeColor = System.Drawing.Color.Green;
                lblTagMessage.Text = "Tag '" + tagName + "' created.";
                BindTags();
            }
            catch (Exception)
            {
                lblTagMessage.ForeColor = System.Drawing.Color.Red;
                lblTagMessage.Text = "Tag already exists or could not be created.";
            }
        }

        protected void btnDeleteTag_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlDeleteTag.SelectedValue))
            {
                lblTagMessage.ForeColor = System.Drawing.Color.Red;
                lblTagMessage.Text = "Select a tag to delete.";
                return;
            }

            long tagId = long.Parse(ddlDeleteTag.SelectedValue);
            bool deleted = _tagService.DeleteTag(tagId);

            if (deleted)
            {
                lblTagMessage.ForeColor = System.Drawing.Color.Green;
                lblTagMessage.Text = "Tag deleted.";
                BindTags();
            }
            else
            {
                lblTagMessage.ForeColor = System.Drawing.Color.Red;
                lblTagMessage.Text = "Cannot delete — tag is used by posts.";
            }
        }

        private void BindTags()
        {
            var tagsData = _tagService.GetAll();
            cblTags.DataSource = tagsData;
            cblTags.DataTextField = "title";
            cblTags.DataValueField = "id";
            cblTags.DataBind();

            ddlDeleteTag.DataSource = tagsData;
            ddlDeleteTag.DataTextField = "title";
            ddlDeleteTag.DataValueField = "id";
            ddlDeleteTag.DataBind();
            ddlDeleteTag.Items.Insert(0, new ListItem("-- Select tag --", string.Empty));
        }

        private void LoadPost(long postId)
        {
            var post = _postService.GetPostById(postId);
            if (post == null)
            {
                lblResult.Text = "Post not found.";
                lblResult.ForeColor = System.Drawing.Color.Red;
                return;
            }

            txtTitle.Text = post.Title;
            txtContent.Text = post.Content;
            _isPublish = post.Published;
            btnPublish.Visible = !_isPublish;
            btnUpdate.Visible = _isPublish;

            var tagIds = _tagService.GetTagIdsForPost(postId);
            foreach (ListItem item in cblTags.Items)
            {
                item.Selected = tagIds.Contains(long.Parse(item.Value));
            }
        }

        private List<long> GetSelectedTagIds()
        {
            var ids = new List<long>();
            foreach (ListItem item in cblTags.Items)
            {
                if (item.Selected)
                {
                    ids.Add(long.Parse(item.Value));
                }
            }

            return ids;
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

        private long SavePost(bool published)
        {
            int authorId = GetLoggedInUserId();
            var tagIds = GetSelectedTagIds();
            long postId;

            if (ViewState["PostId"] != null)
            {
                postId = Convert.ToInt64(ViewState["PostId"]);
                _postService.UpdatePost(postId, authorId, txtTitle.Text, txtContent.Text, published);
            }
            else
            {
                postId = _postService.CreatePost(authorId, txtTitle.Text, txtContent.Text, published);
                ViewState["PostId"] = postId;
            }

            _tagService.SavePostTags(postId, tagIds);
            return postId;
        }

        private bool ValidatePost()
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                ShowError("Title is required.");
                return false;
            }

            if (txtTitle.Text.Trim().Length > 75)
            {
                ShowError("Title must be 75 characters or less.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtContent.Text))
            {
                ShowError("Content is required.");
                return false;
            }

            return true;
        }

        private void ShowError(string message)
        {
            lblResult.ForeColor = System.Drawing.Color.Red;
            lblResult.Text = message;
        }

        private void GoToPostsPage()
        {
            Response.Redirect("Posts.aspx");
        }
    }
}
