<%@ Page Title="Profile" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="NbtBlogEngine.Profile" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row justify-content-center py-4">
        <div class="col-lg-6">

            <h2 class="mb-4">My Profile</h2>

            <div class="card mb-4">
                <div class="card-body">
                    <div class="d-flex align-items-center mb-3">
                        <div class="rounded-circle bg-primary text-white d-flex align-items-center justify-content-center me-3"
                            style="width:60px; height:60px; font-size:1.5rem;">
                            <asp:Label ID="lblInitial" runat="server" />
                        </div>
                        <div>
                            <h5 class="mb-0"><asp:Label ID="lblFullName" runat="server" /></h5>
                            <small class="text-muted"><asp:Label ID="lblEmail" runat="server" /></small>
                        </div>
                    </div>
                    <hr />
                    <div class="row text-center">
                        <div class="col">
                            <h4 class="mb-0"><asp:Label ID="lblPostCount" runat="server" Text="0" /></h4>
                            <small class="text-muted">Posts</small>
                        </div>
                        <div class="col">
                            <small class="text-muted">Member since</small><br />
                            <asp:Label ID="lblRegisteredAt" runat="server" CssClass="fw-semibold" />
                        </div>
                        <div class="col">
                            <small class="text-muted">Last login</small><br />
                            <asp:Label ID="lblLastLogin" runat="server" CssClass="fw-semibold" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-header bg-white fw-semibold">Edit Profile</div>
                <div class="card-body">
                    <div class="row mb-3">
                        <div class="col">
                            <label class="form-label">First Name</label>
                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" style="width:100%" />
                        </div>
                        <div class="col">
                            <label class="form-label">Last Name</label>
                            <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" style="width:100%" />
                        </div>
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Email</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" style="width:100%" />
                    </div>
                    <asp:Button ID="btnSave" runat="server" Text="Save Changes" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                    <asp:Label ID="lblMessage" runat="server" CssClass="d-block mt-2 small" />
                </div>
            </div>

        </div>
    </div>

</asp:Content>
