<%@ Page Title="My Posts" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Posts.aspx.cs" Inherits="NbtBlogEngine.PostList" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="d-flex justify-content-between align-items-center mb-4">
        <h2 class="mb-0">My Posts</h2>
        <asp:Button ID="btnNewPost" runat="server" Text="+ New Post" CssClass="btn btn-primary" OnClick="btnNewPost_Click" />
    </div>

    <div class="card mb-4">
        <div class="card-body py-2">
            <div class="d-flex align-items-center gap-2">
                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control form-control-sm" placeholder="Search posts..." style="max-width: 300px;" />
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnSearch_Click" />
            </div>
        </div>
    </div>

    <asp:ListView ID="lvBlogPosts" runat="server" OnPagePropertiesChanging="lvBlogPosts_PagePropertiesChanging">
        <LayoutTemplate>
            <div class="card">
                <div class="card-body p-0">
                    <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                </div>
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <div class="post-list-item px-3 d-flex justify-content-between align-items-center">
                <div>
                    <asp:HyperLink ID="HyperLink1" runat="server"
                        Text='<%# Eval("title") %>'
                        NavigateUrl='<%# "NewPost.aspx?id=" + Eval("id") %>'
                        CssClass="text-decoration-none fw-semibold" />
                    <div class="post-meta"><%# Eval("createdAt", "{0:MMM dd, yyyy}") %></div>
                </div>
                <div>
                    <%# Convert.ToBoolean(Eval("published"))
                        ? "<span class='status-published'>Published</span>"
                        : "<span class='status-draft'>Draft</span>" %>
                </div>
            </div>
        </ItemTemplate>
        <EmptyDataTemplate>
            <div class="text-center text-muted py-5">
                <h5>No posts yet</h5>
                <p>Click "+ New Post" to create your first post.</p>
            </div>
        </EmptyDataTemplate>
    </asp:ListView>

    <div class="d-flex justify-content-center mt-3">
        <asp:DataPager ID="dpPosts" runat="server" PagedControlID="lvBlogPosts" PageSize="10" class="pagination-container">
            <Fields>
                <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="true" ShowLastPageButton="true"
                    ShowPreviousPageButton="true" ShowNextPageButton="true"
                    FirstPageText="«" PreviousPageText="‹" NextPageText="›" LastPageText="»"
                    ButtonCssClass="btn btn-outline-secondary btn-sm me-1" />
                <asp:NumericPagerField ButtonCount="5" ButtonType="Link"
                    CurrentPageLabelCssClass="btn btn-primary btn-sm me-1"
                    NumericButtonCssClass="btn btn-outline-secondary btn-sm me-1" />
            </Fields>
        </asp:DataPager>
    </div>

</asp:Content>
