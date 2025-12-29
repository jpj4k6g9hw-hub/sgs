<%@ Page Title="订单详情" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="OrderDetail.aspx.cs" Inherits="OrderDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" style="max-width:1000px;">
        <h2>订单详情</h2>
        <asp:Literal ID="litMsg" runat="server" />
        <asp:Panel ID="pnlOrder" runat="server" Visible="false">
            <div style="margin-bottom:12px;">
                订单号：<asp:Label ID="lblOrderId" runat="server" /><br />
                创建时间：<asp:Label ID="lblCreateDate" runat="server" /><br />
                总价：￥<asp:Label ID="lblTotalMoney" runat="server" /><br />
                状态：<asp:Label ID="lblStatus" runat="server" /><br />
            </div>

            <h3>商品明细</h3>
            <asp:Repeater ID="rptOrderItems" runat="server">
                <HeaderTemplate>
                    <table style="width:100%;border-collapse:collapse;">
                        <thead><tr style="background:#fafafa;"><th style="padding:8px;border-bottom:1px solid #eee;">商品</th><th style="padding:8px;border-bottom:1px solid #eee;">单价</th><th style="padding:8px;border-bottom:1px solid #eee;">数量</th><th style="padding:8px;border-bottom:1px solid #eee;">尺码</th></tr></thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td style="padding:8px;border-bottom:1px solid #f0f0f0;">
                            <img src='<%# Eval("PictureUrl") %>' alt="" style="width:60px;height:60px;object-fit:cover;vertical-align:middle;margin-right:8px;" />
                            <%# Eval("Name") %>
                        </td>
                        <td style="padding:8px;border-bottom:1px solid #f0f0f0;">￥<%# Eval("Price") %></td>
                        <td style="padding:8px;border-bottom:1px solid #f0f0f0;"><%# Eval("Number") %></td>
                        <td style="padding:8px;border-bottom:1px solid #f0f0f0;"><%# Eval("size") %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                        </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </asp:Panel>
        <asp:Label ID="lblNoAccess" runat="server" Visible="false" Text="无权限查看该订单或订单不存在。"></asp:Label>
    </div>
</asp:Content>