<%@ Page Title="我的订单" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="MyOrders.aspx.cs" Inherits="MyOrders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" style="max-width:1000px;">
        <h2>我的订单</h2>
        <asp:Literal ID="litMsg" runat="server" />
        <asp:Repeater ID="rptOrders" runat="server" OnItemCommand="rptOrders_ItemCommand" OnItemDataBound="rptOrders_ItemDataBound">
            <HeaderTemplate>
                <table class="orders-table" style="width:100%;border-collapse:collapse;">
                    <thead>
                        <tr style="background:#fafafa;">
                            <th style="padding:10px;border-bottom:1px solid #eee;text-align:left;">订单号</th>
                            <th style="padding:10px;border-bottom:1px solid #eee;text-align:left;">创建时间</th>
                            <th style="padding:10px;border-bottom:1px solid #eee;text-align:left;">总价</th>
                            <th style="padding:10px;border-bottom:1px solid #eee;text-align:left;">数量</th>
                            <th style="padding:10px;border-bottom:1px solid #eee;text-align:left;">状态</th>
                            <th style="padding:10px;border-bottom:1px solid #eee;text-align:left;">操作</th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td style="padding:10px;border-bottom:1px solid #f0f0f0;">
                        <a href="OrderDetail.aspx?orderid=<%# Eval("id") %>"><%# Eval("id") %></a>
                    </td>
                    <td style="padding:10px;border-bottom:1px solid #f0f0f0;"><%# Eval("CreateDate") %></td>
                    <td style="padding:10px;border-bottom:1px solid #f0f0f0;">￥<%# Eval("TotalMoney") %></td>
                    <td style="padding:10px;border-bottom:1px solid #f0f0f0;"><%# Eval("TotalNum") %></td>
                    <td style="padding:10px;border-bottom:1px solid #f0f0f0;"><%# Eval("Status") %></td>
                    <td style="padding:10px;border-bottom:1px solid #f0f0f0;">
                        <asp:LinkButton ID="lnkDetail" runat="server" PostBackUrl='<%# "OrderDetail.aspx?orderid=" + Eval("id") %>'>详情</asp:LinkButton>
                        &nbsp;
                        <asp:LinkButton ID="lnkCancel" runat="server" CommandName="Cancel" CommandArgument='<%# Eval("id") %>' OnClientClick="return confirm('确认取消该订单吗？');">取消订单</asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                    </tbody>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        <asp:Label ID="lblNoOrders" runat="server" Visible="false" Text="您还没有订单。"></asp:Label>
    </div>
</asp:Content>