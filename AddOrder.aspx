<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AddOrder.aspx.cs" Inherits="AddOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>提交订单</h2>
    <asp:GridView ID="gvItems" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="商品" />
            <asp:BoundField DataField="Size" HeaderText="尺码" />
            <asp:BoundField DataField="Price" HeaderText="单价" DataFormatString="{0:C2}" />
            <asp:BoundField DataField="Number" HeaderText="数量" />
        </Columns>
    </asp:GridView>

    收货人: <asp:TextBox ID="txtAddressee" runat="server" /><br />
    收货地址: <asp:TextBox ID="txtAddress" runat="server" Width="400" /><br />
    联系电话: <asp:TextBox ID="txtTel" runat="server" /><br />
    <asp:Button ID="btnSubmitOrder" Text="提交订单" OnClick="btnSubmitOrder_Click" runat="server" />
</asp:Content>