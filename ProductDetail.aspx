<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ProductDetail.aspx.cs" Inherits="ProductDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%-- 使用 Panel 包裹并声明 runat=server（pnlDetail 在后端代码中被访问） --%>
    <asp:Panel ID="pnlDetail" runat="server">
        <div class="container" style="max-width:900px;">
            <div style="display:flex;gap:20px;flex-wrap:wrap;margin-top:16px;">
                <div style="flex:1;min-width:280px;">
                    <asp:Image ID="imgProduct" runat="server" CssClass="product-detail-img" />
                </div>
                <div style="flex:1;min-width:260px;">
                    <h2><asp:Label ID="lblName" runat="server"></asp:Label></h2>
                    <p style="color:var(--muted)"><asp:Label ID="lblBrand" runat="server"></asp:Label></p>
                    <div style="margin:10px 0;font-size:22px;color:var(--accent)">￥<asp:Label ID="lblPrice" runat="server"></asp:Label></div>
                    <p>库存：<asp:Label ID="lblStock" runat="server"></asp:Label></p>
                    <div style="margin-top:12px;">
                        数量：<asp:TextBox ID="txtQty" runat="server" Text="1" Width="60"></asp:TextBox>
                        尺码：<asp:TextBox ID="txtSize" runat="server" Width="80"></asp:TextBox>
                    </div>
                    <div style="margin-top:16px;">
                        <asp:Button ID="btnAddCart" Text="加入购物车" OnClick="btnAddCart_Click" runat="server" CssClass="search-btn" />
                    </div>
                </div>
            </div>

            <div style="margin-top:24px;background:#fff;padding:16px;border-radius:6px;">
                <h3>商品详情</h3>
                <asp:Label ID="lblDesc" runat="server"></asp:Label>
            </div>
        </div>
    </asp:Panel>
</asp:Content>