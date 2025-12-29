using System;
using System.Data;
using MySql.Data.MySqlClient;

public partial class OrderDetail : System.Web.UI.Page
{
    protected int orderId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CurrentUser"] == null)
        {
            Response.Redirect("Login.aspx");
            return;
        }

        if (!int.TryParse(Request.QueryString["orderid"], out orderId) || orderId <= 0)
        {
            lblNoAccess.Visible = true;
            return;
        }

        if (!IsPostBack)
        {
            LoadOrder(orderId);
        }
    }

    private void LoadOrder(int id)
    {
        string user = Session["CurrentUser"] as string;

        // 检查订单是否属于当前用户
        var dtCheck = DBHelper.ExecuteDataTable("SELECT id, TotalMoney, CreateDate, Status, UserName FROM orderInfo WHERE id = @id", new MySqlParameter("@id", id));
        if (dtCheck.Rows.Count == 0)
        {
            lblNoAccess.Visible = true;
            return;
        }
        var row = dtCheck.Rows[0];
        var owner = row["UserName"] == DBNull.Value ? null : row["UserName"].ToString();
        if (!string.Equals(owner, user, StringComparison.OrdinalIgnoreCase))
        {
            // 非本人不可查看（若需要管理员查看可在此加入管理员判断）
            lblNoAccess.Visible = true;
            return;
        }

        // 展示 orderInfo 基本信息
        pnlOrder.Visible = true;
        lblOrderId.Text = row["id"].ToString();
        lblCreateDate.Text = row["CreateDate"].ToString();
        lblTotalMoney.Text = row["TotalMoney"].ToString();
        lblStatus.Text = row["Status"].ToString();

        // 读取 order items（关联 productInfo 获取名称/图片）
        var dtItems = DBHelper.ExecuteDataTable(@"
            SELECT oi.Number, oi.Price, oi.size, p.Name, p.PictureUrl
            FROM orderItem oi
            LEFT JOIN productInfo p ON oi.ProductID = p.id
            WHERE oi.OrderID = @id", new MySqlParameter("@id", id));
        rptOrderItems.DataSource = dtItems;
        rptOrderItems.DataBind();
    }
}