<%@ Page Title="Blog" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Blog.aspx.cs" Inherits="NbtBlogEngine.Blog" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

<div style="margin-top:1rem; margin-bottom:1rem;">

    <div class="d-flex justify-content-between align-items-center mb-4">
        <h2 id="blogTitle" runat="server" class="mb-0">Blog</h2>
        <div class="d-flex align-items-center gap-2">
            <asp:TextBox ID="txtBlogSearch" runat="server" CssClass="form-control form-control-sm" placeholder="Search posts..." style="width:220px" />
            <asp:Button ID="btnBlogSearch" runat="server" Text="Search" CssClass="btn btn-outline-primary btn-sm" OnClick="btnBlogSearch_Click" />
            <a href="Blog.aspx" class="btn btn-outline-secondary btn-sm" id="lnkClearFilter" runat="server" visible="false">Clear</a>
        </div>
    </div>

    <asp:ListView ID="lvBlogPosts" runat="server" OnItemDataBound="lvBlogPosts_ItemDataBound" OnPagePropertiesChanging="lvBlogPosts_PagePropertiesChanging">
        <LayoutTemplate>
            <div>
                <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <div style="margin: 1rem 0 1rem 0;">
            <div class="card post-card">
                <div class="card-body">
                    <div class="post-meta mb-1"><%# Eval("publishedAt", "{0:MMM dd, yyyy}") %></div>
                    <h4 class="card-title">
                        <a href='<%# "PostDetails.aspx?slug=" + Eval("slug") %>'><%# Eval("title") %></a>
                    </h4>
                    <p class="card-text"><%# TruncateContent(Eval("content")?.ToString(), 250) %></p>
                    <div>
                        <asp:Literal ID="litTags" runat="server" />
                    </div>
                </div>
            </div>
                </div>
        </ItemTemplate>
        <EmptyDataTemplate>
            <div class="text-center text-muted py-5">
                <h5>No posts found</h5>
                <p>Check back later for new content.</p>
            </div>
        </EmptyDataTemplate>
    </asp:ListView>

    <div class="d-flex justify-content-center mt-3">
        <asp:DataPager ID="dpBlog" runat="server" PagedControlID="lvBlogPosts" PageSize="5" class="pagination-container">
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

</div>

</asp:Content>
