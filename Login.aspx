<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>登录</h2>
    用户名: <asp:TextBox ID="txtUser" runat="server" /><br />
    密码: <asp:TextBox ID="txtPass" TextMode="Password" runat="server" /><br />
    <asp:Button ID="btnLogin" Text="登录" OnClick="btnLogin_Click" runat="server" />
</asp:Content>