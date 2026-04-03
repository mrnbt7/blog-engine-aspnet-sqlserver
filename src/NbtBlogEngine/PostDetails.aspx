<%@ Page Title="PostDetails" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PostDetails.aspx.cs" Inherits="NbtBlogEngine.PostDetails" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="post-detail py-4">

        <a href="Blog.aspx" class="text-decoration-none text-muted mb-3 d-inline-block">&larr; Back to Blog</a>

        <h1>
            <asp:Label ID="lblTitle" runat="server" />
        </h1>

        <div class="d-flex align-items-center gap-3 mb-4 post-meta" style="font-size: 0.9rem;">
            <span><asp:Label ID="lblCreatedAt" runat="server" /></span>
            <span>&middot;</span>
            <span>
                <asp:Label ID="lblStatus" runat="server" />
            </span>
            <span>&middot;</span>
            <span>By <asp:Label ID="lblAuthorName" runat="server" /></span>
        </div>

        <div class="mb-3">
            <asp:Literal ID="litTags" runat="server" />
        </div>

        <hr />

        <div class="post-body mb-4">
            <asp:Label ID="lblContent" runat="server" />
        </div>

        <asp:HyperLink ID="lnkEdit" runat="server" CssClass="btn btn-outline-primary btn-sm" Text="✏ Edit Post" />

        <!-- Comments Section -->
        <hr class="mt-4" />

        <h4 class="mb-3">
            Comments (<asp:Label ID="lblCommentCount" runat="server" Text="0" />)
        </h4>

        <asp:Repeater ID="rptComments" runat="server">
            <ItemTemplate>
                <div class="card mb-2">
                    <div class="card-body py-2 px-3">
                        <div class="d-flex justify-content-between align-items-center mb-1">
                            <strong class="small"><%# Server.HtmlEncode(Eval("AuthorName")?.ToString()) %></strong>
                            <small class="text-muted"><%# Eval("CreatedAt", "{0:MMM dd, yyyy 'at' hh:mm tt}") %></small>
                        </div>
                        <p class="mb-0"><%# Server.HtmlEncode(Eval("Content")?.ToString()) %></p>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <asp:Panel ID="pnlNoComments" runat="server" CssClass="text-muted mb-3">
            No comments yet. Be the first to comment!
        </asp:Panel>

        <!-- Add Comment Form -->
        <div class="card mt-3" id="pnlAddComment" runat="server">
            <div class="card-header bg-white fw-semibold">Leave a Comment</div>
            <div class="card-body">
                <div class="mb-3">
                    <asp:TextBox ID="txtComment" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"
                        placeholder="Write your comment..." style="width:100%" />
                </div>
                <asp:Button ID="btnSubmitComment" runat="server" Text="Post Comment" CssClass="btn btn-primary btn-sm"
                    OnClick="btnSubmitComment_Click" />
                <asp:Label ID="lblCommentMessage" runat="server" CssClass="d-block mt-2 small" />
            </div>
        </div>

        <asp:Panel ID="pnlLoginToComment" runat="server" CssClass="alert alert-light mt-3" Visible="false">
            <a href="Login.aspx">Login</a> to leave a comment.
        </asp:Panel>

    </div>

</asp:Content>
