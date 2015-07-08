<%@ Page Title="" Language="C#" MasterPageFile="~/masterPage.Master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="home.WEB.Settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <asp:GridView ID="gvSettings" CssClass="centralizeDiv text-capitalize text-center" runat="server" OnRowUpdating="gvSettings_RowUpdating"  AutoGenerateColumns="false" AllowPaging="true" PageSize="5">
            <Columns>
               
                <asp:BoundField DataField="name" HeaderText="Title" />
                <asp:TemplateField HeaderText="Value">
                    <ItemTemplate >
                        <asp:HiddenField runat="server" ID="hdfIndex" Value='<%#Eval("index") %>'  />
                        <asp:TextBox runat="server" placeholder='<%#Eval("value") %>' ID="txtbxValue"></asp:TextBox>
                        
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:ButtonField ButtonType="Link" CommandName="Update" Text="Update" HeaderText="Option"  />
            </Columns>
        </asp:GridView>
    </form>

</asp:Content>
