<%@ Page Title="" Language="C#" MasterPageFile="~/masterPage.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="home.WEB.login" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form0" runat="server">
        
        
         <h1>&nbsp &nbsp  &nbsp  &nbsp  Login
        </h1>
        
        <p>
            Username:
        <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
        </p>
        <p>
            Password:&nbsp 
        <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
        </p>
        <p>
            <asp:CheckBox ID="RememberMe" runat="server" Text="Remember Me" />
        </p>
        <p>
            <asp:Button ID="LoginButton" runat="server" Text="Login" OnClick="LoginButton_Click" />
        </p>
        <p>
            <asp:Label ID="InvalidCredentialsMessage" runat="server" ForeColor="Red" Text="Your username or password is invalid. Please try again."
                Visible="False"></asp:Label>
        </p>
    </form>

</asp:Content>
