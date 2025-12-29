using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Text;

public partial class index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCategories();
            BindProducts();
        }
    }

    private void BindCategories()
    {
        var dt = DBHelper.ExecuteDataTable("SELECT id, Name FROM categoryInfo");
        // 渲染到分类横条（直接构建 HTML）
        var sb = new StringBuilder();
        foreach (DataRow r in dt.Rows)
        {
            sb.AppendFormat("<a class='cat-item' href='index.aspx?categoryId={0}'>{1}</a>", r["id"], r["Name"]);
        }
        // also add "全部"
        sb.Insert(0, "<a class='cat-item' href='index.aspx'>全部</a>");
        // catBar 是在 aspx 中声明为 runat=server 的 div，使用 InnerHtml 填充
        catBar.InnerHtml = sb.ToString();
    }

    private void BindProducts()
    {
        int categoryId = 0;
        string search = Request.QueryString["search"];
        DataTable dt;
        if (!string.IsNullOrEmpty(search))
        {
            dt = DBHelper.ExecuteDataTable("SELECT * FROM productInfo WHERE Name LIKE @k AND Status=1", new MySqlParameter("@k", "%" + search + "%"));
        }
        else if (int.TryParse(Request.QueryString["categoryId"], out categoryId) && categoryId > 0)
        {
            dt = DBHelper.ExecuteDataTable("SELECT * FROM productInfo WHERE CategoryID=@cid AND Status=1", new MySqlParameter("@cid", categoryId));
        }
        else
        {
            dt = DBHelper.ExecuteDataTable("SELECT * FROM productInfo WHERE Status=1");
        }
        rptProducts.DataSource = dt;
        rptProducts.DataBind();
    }

    protected void btnExplore_Click(object sender, EventArgs e)
    {
        // 跳转到全部商品（或做其他行为）
        Response.Redirect("index.aspx");
    }
}