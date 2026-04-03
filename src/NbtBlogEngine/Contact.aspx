<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="NbtBlogEngine.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row justify-content-center py-4">
        <div class="col-lg-6">
            <h2 class="mb-3">Contact Us</h2>
            <div class="card">
                <div class="card-body">
                    <div class="mb-4">
                        <h6 class="text-muted text-uppercase mb-2">Address</h6>
                        <p class="mb-0">One Microsoft Way<br />Redmond, WA 98052-6399</p>
                    </div>
                    <div class="mb-4">
                        <h6 class="text-muted text-uppercase mb-2">Phone</h6>
                        <p class="mb-0">425.555.0100</p>
                    </div>
                    <div>
                        <h6 class="text-muted text-uppercase mb-2">Email</h6>
                        <p class="mb-1"><a href="mailto:support@example.com">support@example.com</a></p>
                        <p class="mb-0"><a href="mailto:marketing@example.com">marketing@example.com</a></p>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
