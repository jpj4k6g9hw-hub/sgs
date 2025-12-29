using System;
using System.Data;
using MySql.Data.MySqlClient;

public partial class MyOrders : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CurrentUser"] == null)
        {
            // 未登录跳转到登录页
            Response.Redirect("Login.aspx");
            return;
        }

        if (!IsPostBack)
        {
            BindOrders();
        }
    }

    private void BindOrders()
    {
        string user = Session["CurrentUser"] as string;
        var dt = DBHelper.ExecuteDataTable("SELECT id, TotalMoney, TotalNum, CreateDate, Status FROM orderInfo WHERE UserName = @user ORDER BY CreateDate DESC", new MySqlParameter("@user", user));
        if (dt.Rows.Count == 0)
        {
            rptOrders.Visible = false;
            lblNoOrders.Visible = true;
        }
        else
        {
            rptOrders.Visible = true;
            lblNoOrders.Visible = false;
            rptOrders.DataSource = dt;
            rptOrders.DataBind();
        }
    }

    protected void rptOrders_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Cancel")
        {
            int orderId;
            if (!int.TryParse((string)e.CommandArgument, out orderId))
            {
                // 有时 CommandArgument 为 object，防护处理
                int.TryParse(e.CommandArgument.ToString(), out orderId);
            }
            if (orderId <= 0) return;

            string user = Session["CurrentUser"] as string;

            // 仅当订单当前状态为 '新提交' 且属于当前用户时才允许取消
            string sql = "UPDATE orderInfo SET Status = '已取消' WHERE id = @id AND UserName = @user AND Status = '新提交'";
            int affected = DBHelper.ExecuteNonQuery(sql, new MySqlParameter("@id", orderId), new MySqlParameter("@user", user));
            if (affected > 0)
            {
                litMsg.Text = "<div style='color:green;margin:8px 0;'>取消成功。</div>";
            }
            else
            {
                litMsg.Text = "<div style='color:red;margin:8px 0;'>取消失败（可能订单已被处理或不存在）。</div>";
            }
            BindOrders();
        }
    }

    protected void rptOrders_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {
        // 控制取消按钮是否可见（仅当 Status == 新提交）
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem)
        {
            var data = e.Item.DataItem as DataRowView;
            var status = data["Status"].ToString();
            var lnkCancel = e.Item.FindControl("lnkCancel") as System.Web.UI.WebControls.LinkButton;
            if (lnkCancel != null)
            {
                lnkCancel.Visible = (status == "新提交");
            }
        }
    }
}