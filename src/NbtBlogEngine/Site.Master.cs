using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;

namespace NbtBlogEngine
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null)
            {
                liLogin.Visible = false;
                liSignup.Visible = false;
                liPosts.Visible = true;
                liUser.Visible = true;
                liLogout.Visible = true;
                lblUserName.Text = Session["UserName"]?.ToString();
            }
            else
            {
                liLogin.Visible = true;
                liSignup.Visible = true;
                liPosts.Visible = false;
                liUser.Visible = false;
                liLogout.Visible = false;
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            Response.Redirect("~/");
        }
    }
}
