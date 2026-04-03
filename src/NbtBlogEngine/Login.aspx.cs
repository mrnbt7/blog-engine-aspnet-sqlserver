using System;
using System.Web.Security;
using System.Web.UI;
using NbtBlogEngine.Services;

namespace NbtBlogEngine
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            var user = ServiceFactory.CreateUserService().ValidateUser(txtEmail.Text.Trim(), txtPassword.Text);

            if (user != null)
            {
                Session["UserId"] = user.Id;
                Session["UserName"] = user.FirstName;
                FormsAuthentication.SetAuthCookie(txtEmail.Text.Trim(), false);
                Response.Redirect(FormsAuthentication.GetRedirectUrl(txtEmail.Text.Trim(), false));
            }
            else
            {
                lblError.Text = "Invalid email or password.";
            }
        }
    }
}
