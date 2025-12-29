using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;

public partial class ShoppingCart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCart();
        }
    }

    private void BindCart()
    {
        List<CartItem> cart = null;
        var user = Session["CurrentUser"] as string;
        if (!string.IsNullOrEmpty(user))
        {
            // 登录用户：从 DB 读取持久化购物车
            cart = CartHelper.LoadCartFromDb(user);
        }
        else
        {
            // 游客：Session 中的购物车
            cart = Session["ShoppingCart"] as List<CartItem> ?? new List<CartItem>();
        }

        // 确保 Session 有购物车副本
        Session["ShoppingCart"] = cart;

        // 绑定到前端 repeater（前端须有 rptCart 控件）
        rptCart.DataSource = cart;
        rptCart.DataBind();

        // 计算总价
        decimal total = cart.Sum(i => i.Price * i.Number);
        lblTotalAmount.Text = total.ToString("F2");
    }

    // Repeater 的 ItemCommand 处理更新或删除
    protected void rptCart_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
    {
        var user = Session["CurrentUser"] as string;
        if (e.CommandName == "Remove")
        {
            // CommandArgument: productId|size
            var arg = (e.CommandArgument ?? "").ToString();
            var parts = arg.Split(new[] { '|' }, 2);
            if (parts.Length >= 1 && int.TryParse(parts[0], out int pid))
            {
                string size = parts.Length == 2 ? parts[1] : "";

                var cart = Session["ShoppingCart"] as List<CartItem> ?? new List<CartItem>();
                var exist = cart.FirstOrDefault(c => c.ProductID == pid && string.Equals(c.Size ?? "", size ?? "", StringComparison.Ordinal));
                if (exist != null)
                {
                    cart.Remove(exist);
                    Session["ShoppingCart"] = cart;

                    if (!string.IsNullOrEmpty(user))
                    {
                        // 删除 DB 中对应项
                        CartHelper.UpdateItemQuantity(user, pid, size, 0);
                    }
                }
            }
            BindCart();
        }
        else if (e.CommandName == "Update")
        {
            // 在模板中应有 txtQty 控件；从命令容器中取出最新数量
            var txtQty = e.Item.FindControl("txtQty") as System.Web.UI.WebControls.TextBox;
            var hfPid = e.Item.FindControl("hfProductId") as System.Web.UI.WebControls.HiddenField;
            var hfSize = e.Item.FindControl("hfSize") as System.Web.UI.WebControls.HiddenField;
            if (txtQty != null && hfPid != null)
            {
                int pid = 0;
                int.TryParse(hfPid.Value, out pid);
                int newQty = 1;
                int.TryParse(txtQty.Text.Trim(), out newQty);
                if (newQty < 0) newQty = 0;
                string size = hfSize != null ? hfSize.Value : "";

                var cart = Session["ShoppingCart"] as List<CartItem> ?? new List<CartItem>();
                var exist = cart.FirstOrDefault(c => c.ProductID == pid && string.Equals(c.Size ?? "", size ?? "", StringComparison.Ordinal));
                if (exist != null)
                {
                    if (newQty == 0) cart.Remove(exist);
                    else exist.Number = newQty;
                    Session["ShoppingCart"] = cart;

                    if (!string.IsNullOrEmpty(user))
                    {
                        CartHelper.UpdateItemQuantity(user, pid, size, newQty);
                    }
                }
            }
            BindCart();
        }
    }

    protected void btnClearCart_Click(object sender, EventArgs e)
    {
        var user = Session["CurrentUser"] as string;
        Session["ShoppingCart"] = new List<CartItem>();
        if (!string.IsNullOrEmpty(user))
        {
            CartHelper.ClearCart(user);
        }
        BindCart();
    }

    protected void btnCheckout_Click(object sender, EventArgs e)
    {
        // 结账：跳转到提交订单页（需要登录）
        if (Session["CurrentUser"] == null)
        {
            // 保存来路然后跳转登录（登录后会合并并可继续下单）
            Response.Redirect("Login.aspx");
            return;
        }
        // 前往下单页面（例如 AddOrder.aspx）
        Response.Redirect("AddOrder.aspx");
    }
}