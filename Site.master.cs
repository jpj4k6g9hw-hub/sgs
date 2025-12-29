using System;
using System.Web.UI;

public partial class Site : MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var user = Session["CurrentUser"] as string;
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(user))
            {
                // 为兼容旧编译器，不使用字符串插值
                litUser.Text = string.Format("<span class='welcome'>欢迎，{0}</span> <a href='/Logout.aspx'>退出</a>", user);
            }
            else
            {
                litUser.Text = "<a href='/Login.aspx'>登录</a> | <a href='/Register.aspx'>注册</a>";
            }
        }
    }

    protected void btnSearchHeader_Click(object sender, EventArgs e)
    {
        string key = txtSearchHeader.Text.Trim();
        if (!string.IsNullOrEmpty(key))
        {
            Response.Redirect("index.aspx?search=" + Server.UrlEncode(key));
        }
    }
}