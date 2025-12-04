using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


//SQL 引用
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace GuestBook
{
    public partial class Login : System.Web.UI.Page
    {
        private string ConnStr = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
        private DataTable SearchUser(string account, string pwd)
        {
            DataTable conntpwd = new DataTable();

            string sql = "SELECT * FROM UserInfo WHERE UserName = @account AND Password = @pwd";         

            

            using (SqlConnection conn = new SqlConnection(ConnStr))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@account", account.ToString());
                cmd.Parameters.AddWithValue("@pwd", pwd.ToString());

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(conntpwd);
            }
            return conntpwd;
        }


        //Show警告
        private void ShowAlert(string message)
        {
            string cleanMessage = message.Replace("'", @"\'").Replace("\r\n", "").Replace("\n", @"\n");
            string script = string.Format(
                "<script type='text/javascript'>alert('{0}');</script>",
                cleanMessage
            );
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", script, false);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //禁止快取帳號密碼
            if (!IsPostBack)
            {
                AcountInput.Text = "";
                PWD.Text = "";
                
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetNoStore();
            }
        }

        protected void lgBt_Click(object sender, EventArgs e)
        {
            string Accountcheck = (AcountInput.Text ?? "" ).ToString();
            string Pwdcheck = (PWD.Text ?? "").ToString();


            if (string.IsNullOrEmpty(Accountcheck) || string.IsNullOrEmpty(Pwdcheck))
            {
                ShowAlert("請輸入帳號與密碼！");
                return;
            }

            DataTable conntpwd = SearchUser(Accountcheck, Pwdcheck);

            if (conntpwd.Rows.Count == 1)
            {
                //!!!!!!!!!!!!! 這邊之後改成用繼承 !!!!!!!!!! Cookie + 簡易加密 或 JWT Token + Cookie（最現代，準備轉 Core 必學）
                //讀取login 是用哪個使用者登入的 ，讓他既成使用者ID 之後其他cs 可以去讀取Session["UserID"]
                Session["UserID"] = Convert.ToInt32(conntpwd.Rows[0]["ID"]);
                //讓他既成使用者ID 之後其他cs 去讀取Session["UserName"]
                Session["UserName"] = conntpwd.Rows[0]["UserName"].ToString();

                Response.Redirect("Default.aspx");
            }
            else
            {
                ShowAlert("帳號或密碼錯誤！請重新輸入");
                PWD.Text = "";
            }
        }
    }
}