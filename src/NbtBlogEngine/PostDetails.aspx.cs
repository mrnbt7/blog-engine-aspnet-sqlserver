using System;
using System.Web.UI;
using NbtBlogEngine.Helpers;
using NbtBlogEngine.Models;
using NbtBlogEngine.Services;

namespace NbtBlogEngine
{
    public partial class PostDetails : System.Web.UI.Page
    {
        private PostService _postService;
        private TagService _tagService;
        private CommentService _commentService;

        protected void Page_Load(object sender, EventArgs e)
        {
            _postService = ServiceFactory.CreatePostService();
            _tagService = ServiceFactory.CreateTagService();
            _commentService = ServiceFactory.CreateCommentService();

            if (!IsPostBack)
            {
                PostDTO post = null;

                string slug = Request.QueryString["slug"];

                if (!string.IsNullOrEmpty(slug))
                {
                    post = _postService.GetPostBySlug(slug);
                }
                else if (long.TryParse(Request.QueryString["id"], out long postId))
                {
                    post = _postService.GetPostById(postId);
                }

                if (post != null)
                {
                    ViewState["PostId"] = post.Id;
                    LoadPost(post);
                    LoadComments(post.Id);
                }
                else
                {
                    lblTitle.Text = "Post not found";
                }
            }

            bool isLoggedIn = Session["UserId"] != null;
            pnlAddComment.Visible = isLoggedIn;
            pnlLoginToComment.Visible = !isLoggedIn;
        }

        protected void btnSubmitComment_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtComment.Text))
            {
                lblCommentMessage.ForeColor = System.Drawing.Color.Red;
                lblCommentMessage.Text = "Comment cannot be empty.";
                return;
            }

            if (ViewState["PostId"] == null)
            {
                return;
            }

            long postId = Convert.ToInt64(ViewState["PostId"]);
            long authorId = Convert.ToInt64(Session["UserId"]);

            _commentService.AddComment(postId, authorId, txtComment.Text.Trim());

            txtComment.Text = string.Empty;
            lblCommentMessage.ForeColor = System.Drawing.Color.Green;
            lblCommentMessage.Text = "Comment posted!";

            LoadComments(postId);
        }

        private void LoadPost(PostDTO post)
        {
            lblTitle.Text = post.Title;
            lblAuthorName.Text = post.AuthorName;
            lblContent.Text = post.Content;
            lblStatus.Text = post.Published ? "Published" : "Draft";
            lblCreatedAt.Text = post.CreatedAt?.ToString("MMM dd, yyyy") ?? string.Empty;
            litTags.Text = TagHelper.RenderBadges(_tagService.GetTagTitlesForPost(post.Id));
            lnkEdit.Visible = Session["UserId"] != null;
            lnkEdit.NavigateUrl = "NewPost.aspx?id=" + post.Id;
        }

        private void LoadComments(long postId)
        {
            var comments = _commentService.GetCommentsByPostId(postId);
            lblCommentCount.Text = comments.Count.ToString();

            if (comments.Count > 0)
            {
                rptComments.DataSource = comments;
                rptComments.DataBind();
                pnlNoComments.Visible = false;
            }
            else
            {
                rptComments.DataSource = null;
                rptComments.DataBind();
                pnlNoComments.Visible = true;
            }
        }
    }
}
