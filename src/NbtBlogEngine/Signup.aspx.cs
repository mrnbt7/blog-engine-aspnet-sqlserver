using System;
using System.Web.UI;
using NbtBlogEngine.Services;

namespace NbtBlogEngine
{
    public partial class Signup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSignup_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                ShowError("First name, email and password are required.");
                return;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                ShowError("Passwords do not match.");
                return;
            }

            try
            {
                bool created = ServiceFactory.CreateUserService().Register(
                    txtFirstName.Text.Trim(),
                    txtLastName.Text.Trim(),
                    txtEmail.Text.Trim(),
                    txtPassword.Text);

                if (created)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    ShowError("Registration failed. Please try again.");
                }
            }
            catch (Exception)
            {
                ShowError("Email already exists. Please use a different email.");
            }
        }

        private void ShowError(string message)
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = message;
        }
    }
}
