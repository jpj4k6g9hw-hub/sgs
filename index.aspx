<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <!-- Hero 区 -->
        <div class="hero">
            <div class="hero-left">
                <h1 style="margin:0 0 12px 0;font-size:28px;">精选儿童服饰 · 舒适与品质并重</h1>
                <p style="color:var(--muted);margin:0 0 16px;">本周热卖 · 新品上架 · 限时促销</p>
                <asp:Button ID="btnExplore" runat="server" Text="立即选购" OnClick="btnExplore_Click" CssClass="search-btn" />
            </div>
            <div class="hero-img">
                <img src="/images/hero.jpg" alt="hero" />
            </div>
        </div>

        <!-- 分类横向条 (必须为服务器控件，以便后端填充) -->
        <div class="cat-bar" id="catBar" runat="server">
            <%-- categories will be rendered by code-behind --%>
        </div>

        <!-- 商品列表 -->
        <div style="margin-top:18px;">
            <h2 style="margin:6px 0 12px 0">推荐商品</h2>
            <asp:Repeater ID="rptProducts" runat="server">
                <ItemTemplate>
                    <div class="product-card">
                        <a href='ProductDetail.aspx?productid=<%# Eval("id") %>' style="text-decoration:none;color:inherit;">
                            <img src='<%# Eval("PictureUrl") %>' alt='<%# Eval("Name") %>' />
                            <div class="product-title"><%# Eval("Name") %></div>
                            <div class="product-price">￥<%# Eval("Price") %></div>
                        </a>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>