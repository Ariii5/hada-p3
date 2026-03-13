<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="proWeb.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #Text1 {
            width: 268px;
        }
        #Text2 {
            width: 268px;
        }
        #Text3 {
            width: 120px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <asp:TextBox ID="TextBox1" runat="server" Font-Size="XX-Large" Width="340px">Products management</asp:TextBox>
    <br />
    <br />
    <asp:TextBox ID="TextBox2" runat="server" Font-Size="Medium" Width="55px">Code</asp:TextBox>
    <input id="Text1" type="text" />
    <br />
    <br />
    <asp:TextBox ID="TextBox3" runat="server" Font-Size="Medium" Width="55px">Name</asp:TextBox>
    <input id="Text2" type="text" />
    <br />
    <br />
    <asp:TextBox ID="TextBox4" runat="server" Font-Size="Medium" Width="56px">Amount</asp:TextBox>
    <input id="Text3" type="text" />
    <br />
    <br />
    <asp:TextBox ID="TextBox5" runat="server" Font-Size="Medium" Width="78px">Category</asp:TextBox>
    <asp:DropDownList ID="DropDownList1" runat="server">
    </asp:DropDownList>
    <br />
    <br />
    <asp:TextBox ID="TextBox6" runat="server" Font-Size="Medium" Width="53px">Price</asp:TextBox>
    <input id="Text4" type="text" />
    <br />
    <br />
    <asp:TextBox ID="TextBox7" runat="server" Font-Size="Medium" Width="109px">Creation Date</asp:TextBox>
    <input id="Text5" type="text" />
    <br />
    <br />
    <input id="Button1" type="button" value="Create" />
    <input id="Button2" type="button" value="Update" />
    <input id="Button3" type="button" value="Delete" />
    <input id="Button4" type="button" value="Read" />
    <input id="Button5" type="button" value="Read First" />
    <input id="Button6" type="button" value="Read Prev" />
    <input id="Button7" type="button" value="Read Next" />
</asp:Content>
