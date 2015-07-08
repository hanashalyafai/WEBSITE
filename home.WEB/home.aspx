<%@ Page Title="" Language="C#" MasterPageFile="~/masterPage.Master" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="home.WEB.home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form0" runat="server">
        <div class="collapse nav navbar-collapse" id="bs-example-navbar-collapse-1">
            <asp:GridView GridLines="None" CssClass="centralizeDiv hyp" ID="gvPlaces" DataKeyNames="HouseID" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="5">
                <Columns>
                    <asp:HyperLinkField ControlStyle-Font-Underline="false"  HeaderText="Place&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" DataTextField="Title" Visible="true" Text="GO" DataNavigateUrlFields="HouseID" DataNavigateUrlFormatString="~/addCommand.aspx?ID={0}" />
                </Columns>
            </asp:GridView>
        </div>

        <!-- ControlStyle-CssClass="hyp collapse nav navbar-collapse" alert-danger -->
    </form>


    <style type="text/css">
        .hyp {
            text-decoration: none;
            text-transform:capitalize;
        }

        a:hover {
            color: #000000;
            font-weight: bold;
        }
    </style>

</asp:Content>
