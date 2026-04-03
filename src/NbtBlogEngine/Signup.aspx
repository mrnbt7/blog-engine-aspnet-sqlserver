<%@ Page Title="Sign Up" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="NbtBlogEngine.Signup" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row justify-content-center" style="padding-top:5vh;">
        <div class="col-11 col-sm-8 col-md-6 col-lg-4">
            <div class="card shadow-sm">
                <div class="card-body p-4">
                    <h3 class="text-center mb-4">Create Account</h3>

                    <div class="row mb-3">
                        <div class="col-6">
                            <label class="form-label">First Name</label>
                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" placeholder="First name" style="width:100%" />
                        </div>
                        <div class="col-6">
                            <label class="form-label">Last Name</label>
                            <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" placeholder="Last name" style="width:100%" />
                        </div>
                    </div>

                    <div class="row mb-3">
                        <div class="col-12">
                            <label class="form-label">Email</label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="you@example.com" style="width:100%" />
                        </div>
                    </div>

                    <div class="row mb-3">
                        <div class="col-12">
                            <label class="form-label">Password</label>
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Create password" style="width:100%" />
                        </div>
                    </div>

                    <div class="row mb-3">
                        <div class="col-12">
                            <label class="form-label">Confirm Password</label>
                            <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Confirm password" style="width:100%" />
                        </div>
                    </div>

                    <div class="row mb-3">
                        <div class="col-12">
                            <asp:Button ID="btnSignup" runat="server" Text="Sign Up" CssClass="btn btn-primary" OnClick="btnSignup_Click" style="width:100%" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-12 text-center">
                            <asp:Label ID="lblMessage" runat="server" CssClass="d-block mb-2" />
                            <span class="text-muted">Already have an account? <a href="Login.aspx">Login</a></span>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

</asp:Content>
