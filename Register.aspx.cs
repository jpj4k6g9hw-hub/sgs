using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;

public partial class Register : System.Web.UI.Page
{
    protected void btnReg_Click(object sender, EventArgs e)
    {
        string user = txtUser.Text.Trim();
        string pass = txtPass.Text.Trim();
        string email = txtEmail.Text.Trim();
        string tel = txtTel.Text.Trim();
        string post = txtPost.Text.Trim();

        if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass)) { Response.Write("<script>alert('用户名和密码必填');</script>"); return; }

        var dt = DBHelper.ExecuteDataTable("SELECT Username FROM userInfo WHERE Username=@u", new MySqlParameter("@u", user));
        if (dt.Rows.Count > 0) { Response.Write("<script>alert('用户名已存在');</script>"); return; }

        DBHelper.ExecuteNonQuery("INSERT INTO userInfo (Role,Username,Password,Email,Telephone,Postcode) VALUES ('member',@u,@p,@e,@t,@post)",
            new MySqlParameter("@u", user),
            new MySqlParameter("@p", pass),
            new MySqlParameter("@e", email),
            new MySqlParameter("@t", tel),
            new MySqlParameter("@post", post));

        Response.Write("<script>alert('注册成功，请登录');window.location='Login.aspx';</script>");
    }
}