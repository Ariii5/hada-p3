<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="proWeb.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="product-form">
    <h2>Products Management</h2>

    <div class="form-group">
        <label for="tbCode">Code</label>
        <asp:TextBox ID="tbCode" runat="server" CssClass="input-box" />
    </div>

    <div class="form-group">
        <label for="tbName">Name</label>
        <asp:TextBox ID="tbName" runat="server" CssClass="input-box" />
    </div>

    <div class="form-group">
        <label for="tbAmount">Amount</label>
        <asp:TextBox ID="tbAmount" runat="server" CssClass="input-box small-input" />
    </div>

    <div class="form-group">
        <label for="ddlCategory">Category</label>
        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="input-box" />
    </div>

    <div class="form-group">
        <label for="tbPrice">Price</label>
        <asp:TextBox ID="tbPrice" runat="server" CssClass="input-box small-input" />
    </div>

    <div class="form-group">
        <label for="tbCreationDate">Creation Date</label>
        <asp:TextBox ID="tbCreationDate" runat="server" CssClass="input-box" />
    </div>

    <div class="button-group">
        <asp:Button ID="btnCreate" runat="server" Text="Create" CssClass="btn" OnClick="btnCreate_Click" />
        <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn" OnClick="btnUpdate_Click" />
        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn" OnClick="btnDelete_Click" />
    </div>

    <div class="button-group">
        <asp:Button ID="btnRead" runat="server" Text="Read" CssClass="btn" OnClick="btnRead_Click" />
        <asp:Button ID="btnReadFirst" runat="server" Text="Read First" CssClass="btn" OnClick="btnReadFirst_Click" />
        <asp:Button ID="btnReadPrev" runat="server" Text="Read Prev" CssClass="btn" OnClick="btnReadPrev_Click" />
        <asp:Button ID="btnReadNext" runat="server" Text="Read Next" CssClass="btn" OnClick="btnReadNext_Click" />
    </div>

    <asp:Label ID="lblMessage" runat="server" CssClass="lblMessage" />
    </div>
</asp:Content>