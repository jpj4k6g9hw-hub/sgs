<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ShoppingCart.aspx.cs" Inherits="ShoppingCart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>购物车</h2>
    <asp:GridView ID="gvCart" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="商品" />
            <asp:BoundField DataField="Size" HeaderText="尺码" />
            <asp:BoundField DataField="Price" HeaderText="单价" DataFormatString="{0:C2}" />
            <asp:TemplateField HeaderText="数量">
                <ItemTemplate>
                    <asp:TextBox ID="txtNum" runat="server" Text='<%# Eval("Number") %>' Width="50"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkRemove" runat="server" CommandName="Remove" CommandArgument='<%# Eval("ProductID") + "|" + Eval("Size") %>' OnCommand="lnkRemove_Command">删除</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:Button ID="btnUpdate" Text="更新数量" OnClick="btnUpdate_Click" runat="server" />
    <asp:Button ID="btnCheckout" Text="提交订单" OnClick="btnCheckout_Click" runat="server" />
</asp:Content>