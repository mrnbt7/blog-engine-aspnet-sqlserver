<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="NbtBlogEngine._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="hero text-center">
        <h1>Welcome to NBT Blog</h1>
        <p class="mb-3">A simple, modern blog engine built with ASP.NET Web Forms</p>
        <a href="Blog.aspx" class="btn btn-light btn-lg px-4">Read the Blog</a>
    </div>

    <h4 class="mb-3">Recent Posts</h4>

    <asp:ListView ID="lvBlogPosts" runat="server" OnPagePropertiesChanging="lvBlogPosts_PagePropertiesChanging">
        <LayoutTemplate>
            <div class="row">
                <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <div class="col-md-6 col-lg-4 mb-4">
                <div class="card h-100 post-card">
                    <div class="card-body d-flex flex-column">
                        <h5 class="card-title">
                            <a href='<%# "PostDetails.aspx?slug=" + Eval("slug") %>'><%# Eval("title") %></a>
                        </h5>
                        <p class="card-text flex-grow-1"><%# TruncateContent(Eval("content")?.ToString(), 120) %></p>
                        <div class="post-meta mt-auto"><%# Eval("publishedAt", "{0:MMM dd, yyyy}") %></div>
                    </div>
                </div>
            </div>
        </ItemTemplate>
        <EmptyDataTemplate>
            <div class="text-center text-muted py-5">No posts yet.</div>
        </EmptyDataTemplate>
    </asp:ListView>

    <div class="d-flex justify-content-center mt-3">
        <asp:DataPager ID="dpHome" runat="server" PagedControlID="lvBlogPosts" PageSize="6" class="pagination-container">
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
