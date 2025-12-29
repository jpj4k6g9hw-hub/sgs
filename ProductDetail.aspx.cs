using System;
using System.Data;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

public partial class ProductDetail : System.Web.UI.Page
{
    protected int productId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!int.TryParse(Request.QueryString["productid"], out productId))
        {
            pnlDetail.Visible = false;
            return;
        }
        if (!IsPostBack)
        {
            LoadProduct(productId);
        }
    }

    private void LoadProduct(int id)
    {
        var dt = DBHelper.ExecuteDataTable("SELECT * FROM productInfo WHERE id=@id", new MySqlParameter("@id", id));
        if (dt.Rows.Count == 0) { pnlDetail.Visible = false; return; }
        var row = dt.Rows[0];
        imgProduct.ImageUrl = row["PictureUrl"].ToString();
        lblName.Text = row["Name"].ToString();
        lblPrice.Text = row["Price"].ToString();
        lblBrand.Text = row["Brand"].ToString();
        lblStock.Text = row["Stock"].ToString();
        lblDesc.Text = string.Format("{0} — 适合 {1}", row["Name"].ToString(), row["ForAges"].ToString());
    }

    protected void btnAddCart_Click(object sender, EventArgs e)
    {
        int qty = 1;
        int.TryParse(txtQty.Text.Trim(), out qty);
        string size = txtSize.Text.Trim();

        // 读取商品基础信息（再次确认库存、价格等）
        var dt = DBHelper.ExecuteDataTable("SELECT id, Name, PictureUrl, Price, Stock FROM productInfo WHERE id=@id", new MySqlParameter("@id", productId));
        if (dt.Rows.Count == 0)
        {
            // 商品不存在
            Response.Write("<script>alert('商品不存在');history.back();</script>");
            return;
        }
        var row = dt.Rows[0];
        decimal price = Convert.ToDecimal(row["Price"]);
        int stock = row["Stock"] == DBNull.Value ? 0 : Convert.ToInt32(row["Stock"]);
        if (qty <= 0) qty = 1;
        if (stock > 0 && qty > stock)
        {
            // 可以根据业务提示库存不足，但这里仅做简单处理
            qty = stock;
        }

        var item = new CartItem
        {
            ProductID = productId,
            Name = row["Name"].ToString(),
            PictureUrl = row["PictureUrl"].ToString(),
            Price = price,
            Number = qty,
            Size = size
        };

        // 更新 Session 购物车
        List<CartItem> cart = Session["ShoppingCart"] as List<CartItem>;
        if (cart == null) cart = new List<CartItem>();

        // 合并到 session cart：相同 ProductID + Size 则累加
        var existing = cart.Find(c => c.ProductID == item.ProductID && string.Equals(c.Size ?? "", item.Size ?? "", StringComparison.Ordinal));
        if (existing != null)
        {
            existing.Number += item.Number;
        }
        else
        {
            cart.Add(item);
        }

        Session["ShoppingCart"] = cart;

        // 如果登录用户，持久化新增项到 DB（AddOrUpdateItem 会累加数量）
        var user = Session["CurrentUser"] as string;
        if (!string.IsNullOrEmpty(user))
        {
            try
            {
                CartHelper.AddOrUpdateItem(user, item);
            }
            catch
            {
                // 忽略 DB 写入错误，但应记录日志；保持 Session 中的购物车可用
            }
        }

        // 跳转到购物车页
        Response.Redirect("ShoppingCart.aspx");
    }
}