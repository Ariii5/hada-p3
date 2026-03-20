<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="proWeb.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <h2>Products management</h2>
    <br />
    <label>Code</label>
    <asp:TextBox ID="tbCode" runat="server" Width="268px" />
    <br /><br />
    <label>Name</label>
    <asp:TextBox ID="tbName" runat="server" Width="268px" />
    <br /><br />
    <label>Amount</label>
    <asp:TextBox ID="tbAmount" runat="server" Width="120px" />
    <br /><br />
    <label>Category</label>
    <asp:DropDownList ID="ddlCategory" runat="server" />
    <br /><br />
    <label>Price</label>
    <asp:TextBox ID="tbPrice" runat="server" Width="120px" />
    <br /><br />
    <label>Creation Date</label>
    <asp:TextBox ID="tbCreationDate" runat="server" Width="268px" />
    <br /><br />
    <asp:Button ID="btnCreate" runat="server" Text="Create" OnClick="btnCreate_Click" />
    <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
    <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
    <asp:Button ID="btnRead" runat="server" Text="Read" OnClick="btnRead_Click" />
    <asp:Button ID="btnReadFirst" runat="server" Text="Read First" OnClick="btnReadFirst_Click" />
    <asp:Button ID="btnReadPrev" runat="server" Text="Read Prev" OnClick="btnReadPrev_Click" />
    <asp:Button ID="btnReadNext" runat="server" Text="Read Next" OnClick="btnReadNext_Click" />
    <br /><br />
    <asp:Label ID="lblMessage" runat="server" />
</asp:Content>