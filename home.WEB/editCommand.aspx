<%@ Page Title="" Language="C#" MasterPageFile="~/masterPage.Master" AutoEventWireup="true" CodeBehind="editCommand.aspx.cs" Inherits="home.WEB.editCommand" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <asp:DropDownList ID="DDLyear" runat="server">
        </asp:DropDownList>
        <asp:DropDownList ID="DDLmonth" runat="server"></asp:DropDownList>
        <asp:DropDownList ID="DDLday" runat="server"></asp:DropDownList>
        <asp:DropDownList ID="DDLhour" runat="server"></asp:DropDownList>
        <asp:DropDownList ID="DDLminute" runat="server"></asp:DropDownList>
        <asp:DropDownList ID="DDLsecond" runat="server"></asp:DropDownList>
        <asp:DropDownList ID="DDLstate" runat="server"></asp:DropDownList>
        <asp:Button runat="server" ID="btnSave" Text="Update" OnClick="btnSave_Click" /> 
    </form>

</asp:Content>
