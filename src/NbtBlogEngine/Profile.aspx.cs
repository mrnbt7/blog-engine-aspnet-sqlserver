using System;
using System.Web.UI;
using NbtBlogEngine.Services;

namespace NbtBlogEngine
{
    public partial class Profile : System.Web.UI.Page
    {
        private UserService _userService;

        protected void Page_Load(object sender, EventArgs e)
        {
            _userService = ServiceFactory.CreateUserService();

            if (Session["UserId"] == null)
            {
                Response.Redirect("~/Login");
                return;
            }

            if (!IsPostBack)
            {
                LoadProfile();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "First name and email are required.";
                return;
            }

            long userId = Convert.ToInt64(Session["UserId"]);

            try
            {
                bool updated = _userService.UpdateProfile(
                    userId,
                    txtFirstName.Text.Trim(),
                    txtLastName.Text.Trim(),
                    txtEmail.Text.Trim());

                if (updated)
                {
                    Session["UserName"] = txtFirstName.Text.Trim();
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Text = "Profile updated.";
                    LoadProfile();
                }
                else
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "Update failed.";
                }
            }
            catch (Exception)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Email already in use.";
            }
        }

        private void LoadProfile()
        {
            long userId = Convert.ToInt64(Session["UserId"]);
            var user = _userService.GetProfile(userId);

            if (user == null)
            {
                Response.Redirect("~/");
                return;
            }

            string fullName = (user.FirstName + " " + user.LastName).Trim();
            lblFullName.Text = fullName;
            lblEmail.Text = user.Email;
            lblInitial.Text = !string.IsNullOrEmpty(user.FirstName) ? user.FirstName.Substring(0, 1).ToUpperInvariant() : "?";
            lblPostCount.Text = user.PostCount.ToString();
            lblRegisteredAt.Text = user.RegisteredAt?.ToString("MMM dd, yyyy") ?? "N/A";
            lblLastLogin.Text = user.LastLogin?.ToString("MMM dd, yyyy") ?? "N/A";

            txtFirstName.Text = user.FirstName;
            txtLastName.Text = user.LastName;
            txtEmail.Text = user.Email;
        }
    }
}
