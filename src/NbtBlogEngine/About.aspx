<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="NbtBlogEngine.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row justify-content-center py-4">
        <div class="col-lg-8">
            <h2 class="mb-3">About NbtBlog</h2>
            <div class="card">
                <div class="card-body">
                    <p class="lead">NbtBlog is a lightweight blog engine built with ASP.NET Web Forms, SQL Server, and Bootstrap 5.</p>
                    <p>It supports creating, editing, and publishing posts with tags, user authentication, and a clean public-facing blog.</p>
                    <h5 class="mt-4">Features</h5>
                    <ul>
                        <li>Create, edit, publish, and delete posts</li>
                        <li>Tag-based categorization with filtering</li>
                        <li>User registration and login</li>
                        <li>Auto-generated slugs for SEO-friendly URLs</li>
                        <li>Public blog view and admin dashboard</li>
                    </ul>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
