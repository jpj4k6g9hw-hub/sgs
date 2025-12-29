# OnlineShop (ASP.NET WebForms + MySQL) — 儿童服装商城示例

说明：
- Framework: .NET Framework 4.8.1
- 数据库: MySQL, DB name: `onlineshop`
- MySQL 用户: `root`
- MySQL 密码: `123456`
- NuGet: 安装 MySql.Data

快速部署步骤：
1. 在 MySQL 中执行 `db_init.sql` 创建数据库和测试数据。
2. 在 Visual Studio 新建 ASP.NET Web Forms (.NET Framework 4.8.1) 项目。
3. 将本仓库文件复制到项目中（替换默认文件）。
4. 在项目中通过 NuGet 安装 `MySql.Data`。
5. 检查 `Web.config` 中的连接字符串是否与本机 MySQL 配置匹配。
6. 运行 `index.aspx`。

主要页面：
- index.aspx: 商品列表（按类别筛选）
- ProductDetail.aspx: 商品详情 -> 添加购物车
- ShoppingCart.aspx: 查看/编辑购物车 -> 提交结算（转到 AddOrder.aspx）
- AddOrder.aspx: 填写订单信息并提交
- Register.aspx / Login.aspx: 会员注册与登录
- Admin/ 登录页与后台管理页面（商品/类别/订单/会员）

注意：
- 示例注重功能演示与数据流，页面样式较简单，欢迎按需美化。