<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="NbtBlogEngine.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row justify-content-center" style="padding-top:5vh;">
        <div class="col-11 col-sm-8 col-md-6 col-lg-4">
            <div class="card shadow-sm">
                <div class="card-body p-4">
                    <h3 class="text-center mb-4">Welcome Back</h3>

                    <div class="row mb-3">
                        <div class="col-12">
                            <label class="form-label">Email</label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="you@example.com" style="width:100%" />
                        </div>
                    </div>

                    <div class="row mb-3">
                        <div class="col-12">
                            <label class="form-label">Password</label>
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter password" style="width:100%" />
                        </div>
                    </div>

                    <div class="row mb-3">
                        <div class="col-12">
                            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary" OnClick="btnLogin_Click" style="width:100%" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-12 text-center">
                            <asp:Label ID="lblError" runat="server" ForeColor="Red" CssClass="d-block mb-2" />
                            <span class="text-muted">Don't have an account? <a href="Signup.aspx">Sign Up</a></span>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

</asp:Content>
