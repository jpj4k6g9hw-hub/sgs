using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public partial class AddOrder : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) BindItems();
    }

    private void BindItems()
    {
        var items = Session["ShoppingCart2"] as List<CartItem>;
        if (items == null) items = new List<CartItem>();
        gvItems.DataSource = items;
        gvItems.DataBind();
    }

    protected void btnSubmitOrder_Click(object sender, EventArgs e)
    {
        var user = Session["CurrentUser"] as string;
        if (string.IsNullOrEmpty(user)) { Response.Redirect("Login.aspx?returnUrl=AddOrder.aspx"); return; }

        var items = Session["ShoppingCart2"] as List<CartItem>;
        if (items == null || items.Count == 0) { Response.Write("<script>alert('购物车为空');</script>"); return; }

        decimal totalMoney = 0; int totalNum = 0;
        foreach (var it in items) { totalMoney += it.Price * it.Number; totalNum += it.Number; }

        string addressee = txtAddressee.Text.Trim();
        string address = txtAddress.Text.Trim();
        string tel = txtTel.Text.Trim();

        // 插入 orderInfo
        string sqlOrder = "INSERT INTO orderInfo (TotalMoney, TotalNum, UserName, Addressee, Address, Tel, Status) VALUES (@money,@num,@user,@addressee,@address,@tel,'新提交'); SELECT LAST_INSERT_ID();";
        var param = new MySqlParameter[]
        {
            new MySqlParameter("@money", totalMoney),
            new MySqlParameter("@num", totalNum),
            new MySqlParameter("@user", user),
            new MySqlParameter("@addressee", addressee),
            new MySqlParameter("@address", address),
            new MySqlParameter("@tel", tel)
        };

        object obj = DBHelper.ExecuteScalar(sqlOrder, param);
        int orderId = Convert.ToInt32(obj);

        // 插入 orderItem
        foreach (var it in items)
        {
            string sqlItem = "INSERT INTO orderItem (OrderID, ProductID, Number, Price, size) VALUES (@oid,@pid,@num,@price,@size)";
            DBHelper.ExecuteNonQuery(sqlItem,
                new MySqlParameter("@oid", orderId),
                new MySqlParameter("@pid", it.ProductID),
                new MySqlParameter("@num", it.Number),
                new MySqlParameter("@price", it.Price),
                new MySqlParameter("@size", it.Size));
            // 可选：减少库存
            DBHelper.ExecuteNonQuery("UPDATE productInfo SET Stock = Stock - @n WHERE id=@pid", new MySqlParameter("@n", it.Number), new MySqlParameter("@pid", it.ProductID));
        }

        // 清空购物车
        Session["ShoppingCart"] = null;
        Session["ShoppingCart2"] = null;

        Response.Write("<script>alert('订单提交成功');window.location='index.aspx';</script>");
    }
}