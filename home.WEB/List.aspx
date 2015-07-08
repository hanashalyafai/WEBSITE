<%@ Page Title="" Language="C#" MasterPageFile="~/masterPage.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="home.WEB.List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form0" runat="server">
        
       
            <asp:GridView runat="server" CssClass="text-capitalize centralizeDiv" OnPageIndexChanging="gvList_PageIndexChanging" ID="gvList" AutoGenerateColumns="false" AllowPaging="true" PageSize="5">
                <Columns>
                    <asp:BoundField HeaderText="Place" DataField="title" DataFormatString="" />
                    <asp:BoundField HeaderText="Appliance" DataField="name" DataFormatString="" />
                    <asp:BoundField HeaderText="State" DataField="state" DataFormatString="" />
                    <asp:BoundField HeaderText="Time" DataField="time" />

                    <asp:TemplateField HeaderText="Option">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlEdit" runat="server" NavigateUrl='<%#Eval("ListID","editCommand.aspx?ListID={0}") %>' Text="Edit"></asp:HyperLink>
                            <asp:linkbutton id="lbDelete" commandargument='<%# Eval("ListID") %>' runat="server" db='<%#Eval("ListID","?ListID={0}") %>' onclick="lbDelete_Click" xmlns:asp="#unknown">Delete</asp:linkbutton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
       
    </form>
    
</asp:Content>
