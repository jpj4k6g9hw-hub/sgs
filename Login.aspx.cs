using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // 如果已经登录，直接跳转首页
        if (!IsPostBack && Session["CurrentUser"] != null)
        {
            Response.Redirect("index.aspx");
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string username = (txtUsername?.Text ?? "").Trim();
        string password = (txtPassword?.Text ?? "").Trim();

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            lblMsg.Text = "请输入用户名和密码。";
            return;
        }

        try
        {
            var dt = DBHelper.ExecuteDataTable("SELECT Username, Password, Role FROM userInfo WHERE Username = @u", new MySqlParameter("@u", username));
            if (dt.Rows.Count == 0)
            {
                lblMsg.Text = "用户名或密码错误。";
                return;
            }
            var row = dt.Rows[0];
            var storedPwd = row["Password"] == DBNull.Value ? "" : row["Password"].ToString();

            // 当前项目使用明文密码存储（若已切换为哈希，请相应修改验证逻辑）
            if (!string.Equals(password, storedPwd, StringComparison.Ordinal))
            {
                lblMsg.Text = "用户名或密码错误。";
                return;
            }

            // 登录成功
            Session["CurrentUser"] = username;
            Session["CurrentUserRole"] = row["Role"] == DBNull.Value ? "member" : row["Role"].ToString();

            // 合并 session 购物车到 DB（若 session 中存在购物车）
            var sessionCart = Session["ShoppingCart"] as System.Collections.Generic.List<CartItem>;
            var merged = CartHelper.MergeSessionCartToDb(username, sessionCart);
            // 把合并后的购物车写回 Session
            Session["ShoppingCart"] = merged;

            // 登录后跳回首页或上次页面
            Response.Redirect("index.aspx");
        }
        catch (Exception ex)
        {
            // 简单错误提示（生产应有日志记录）
            lblMsg.Text = "登录时发生错误：" + ex.Message;
        }
    }
}