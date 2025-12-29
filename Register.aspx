<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>会员注册</h2>
    用户名: <asp:TextBox ID="txtUser" runat="server" /><br />
    密码: <asp:TextBox ID="txtPass" TextMode="Password" runat="server" /><br />
    邮箱: <asp:TextBox ID="txtEmail" runat="server" /><br />
    电话: <asp:TextBox ID="txtTel" runat="server" /><br />
    邮编: <asp:TextBox ID="txtPost" runat="server" /><br />
    <asp:Button ID="btnReg" Text="注册" OnClick="btnReg_Click" runat="server" />
</asp:Content>