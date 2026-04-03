<%@ Page Title="Post Editor" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewPost.aspx.cs" Inherits="NbtBlogEngine.NewPost" ValidateRequest="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-4">
        <h2 class="mb-0">Post Editor</h2>
        <asp:Button ID="btnBack" runat="server" Text="&larr; Back" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnBack_Click" />
    </div>

    <div class="row">
        <div class="col-lg-8">
            <div class="card mb-3">
                <div class="card-body">
                    <div class="mb-3">
                        <label class="form-label fw-semibold">Title</label>
                        <asp:TextBox ID="txtTitle" CssClass="form-control w-100"  runat="server" placeholder="Enter post title..." style="max-width: 52rem;" />
                        <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle"
                            ErrorMessage="Title is required." CssClass="text-danger small" Display="Dynamic" ValidationGroup="PostForm" />
                        <asp:RegularExpressionValidator ID="revTitle" runat="server" ControlToValidate="txtTitle"
                            ValidationExpression=".{1,75}" ErrorMessage="Title must be 75 characters or less."
                            CssClass="text-danger small" Display="Dynamic" ValidationGroup="PostForm" />
                    </div>
                    <div class="mb-3">
                        <label class="form-label fw-semibold">Content</label>
                        <asp:TextBox ID="txtContent" CssClass="form-control w-100" style="max-width: 52rem;" runat="server" TextMode="MultiLine" Rows="12" placeholder="Write your post content..." ValidateRequestMode="Disabled" />
                        <asp:RequiredFieldValidator ID="rfvContent" runat="server" ControlToValidate="txtContent"
                            ErrorMessage="Content is required." CssClass="text-danger small" Display="Dynamic" ValidationGroup="PostForm" />
                    </div>
                </div>
            </div>
        </div>

    <script src="<%= ResolveUrl("~/Scripts/tinymce/tinymce.min.js") %>"></script>
    <script type="text/javascript">
        tinymce.init({
            selector: '#<%= txtContent.ClientID %>',
            height: 400,
            menubar: false,
            plugins: 'lists link image code table wordcount',
            toolbar: 'undo redo | bold italic underline strikethrough | bullist numlist | link image table | code',
            setup: function (editor) {
                editor.on('change', function () {
                    editor.save();
                });
            }
        });
    </script>

        <div class="col-lg-4">
            <div class="card mb-3">
                <div class="card-header bg-white fw-semibold">Actions</div>
                <div class="card-body d-grid gap-2">
                    <asp:Button ID="btnPublish" runat="server" Text="Publish" CssClass="btn btn-primary" OnClick="btnPublish_Click" ValidationGroup="PostForm" />
                    <asp:Button ID="btnSaveDraft" runat="server" Text="Save as Draft" CssClass="btn btn-outline-secondary" OnClick="btnSaveDraft_Click" ValidationGroup="PostForm" />
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-primary" OnClick="btnUpdate_Click" ValidationGroup="PostForm" />
                    <asp:Button ID="btnPreview" runat="server" Text="Preview" CssClass="btn btn-outline-info" OnClick="btnPreview_Click" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-outline-danger" OnClick="btnDelete_Click"
                        OnClientClick="return confirm('Are you sure you want to delete this post?');" />
                </div>
            </div>

            <div class="card mb-3">
                <div class="card-header bg-white fw-semibold">Tags</div>
                <div class="card-body">
                    <asp:CheckBoxList ID="cblTags" runat="server" CssClass="list-unstyled" RepeatLayout="Flow" style="margin-right: 0.4rem;" />
                    <hr />
                    <div class="d-flex gap-2">
                        <asp:TextBox ID="txtNewTag" runat="server" CssClass="form-control form-control-sm" placeholder="New tag name" />
                        <asp:Button ID="btnAddTag" runat="server" Text="Add" CssClass="btn btn-outline-primary btn-sm" OnClick="btnAddTag_Click" />
                    </div>
                    <div class="d-flex gap-2 mt-2">
                        <asp:DropDownList ID="ddlDeleteTag" runat="server" CssClass="form-select form-select-sm" />
                        <asp:Button ID="btnDeleteTag" runat="server" Text="Delete" CssClass="btn btn-outline-danger btn-sm"
                            OnClick="btnDeleteTag_Click" OnClientClick="return confirm('Delete this tag?');" />
                    </div>
                    <asp:Label ID="lblTagMessage" runat="server" CssClass="d-block mt-1 small" />
                </div>
            </div>

                    <asp:ValidationSummary ID="vsSummary" runat="server" ValidationGroup="PostForm"
                        CssClass="alert alert-danger small mt-2" HeaderText="Please fix the following:" />
                    <asp:Label ID="lblResult" runat="server" CssClass="d-block mt-2" />
        </div>
    </div>

</asp:Content>
