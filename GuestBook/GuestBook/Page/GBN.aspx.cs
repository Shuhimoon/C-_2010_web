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


namespace GuestBook.Styles
{
    public partial class GBN : System.Web.UI.Page
    {

        private string ConnStr = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
        private void CreateGuestBook(string title,string gbook)
        {
            DataTable dt = new DataTable();

            //讀取login 是用哪個使用者登入的 ，使用繼承User.cs 去讀取Session["UserID"]
            int userId = Convert.ToInt32(Session["UserID"]);
            
            string sql = @"
                INSERT INTO GusetBook (Title, GuestBook , User_ID, CreateDate)
                VALUES (@Title, @Guestbook , @UserID, GETDATE());";   // 請根據你的實際表格欄位調整

            using (SqlConnection conn = new SqlConnection(ConnStr))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@UserID", userId);

                // 防止 XSS：儲存前進行 HTML Encode（推薦方式）
                // 這樣前端顯示時就不會執行 <script> 或其他 HTML 標籤
                cmd.Parameters.AddWithValue("@Title", Server.HtmlEncode(title.Trim()));
                cmd.Parameters.AddWithValue("@Guestbook", Server.HtmlEncode(gbook.Trim()));


                conn.Open();
                cmd.ExecuteNonQuery();
            }


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
            if (!IsPostBack)   // 避免重新整理又重跑
            {
                txtCDate.Text = DateTime.Now.ToString("yyyy/MM/dd  HH:mm:ss");
                
                if (Session["UserID"] == null)
                {
                    Response.Redirect("~/Login.aspx"); // 沒登入就踢回去登入頁
                }
            }
        }

        protected void btnC_Click(object sender, EventArgs e)
        {
            DateTime start = Convert.ToDateTime(txtCDate.Text);

            
            //正確判斷：標題有沒有 ， 有的話為true
            if (string.IsNullOrWhiteSpace(txtNTitle.Text))
            {
                ShowAlert("請輸入標題！");
                return;
            }

            try
            {
                CreateGuestBook(txtNTitle.Text, txtNGB.Text);

                ShowAlert("留言成功！");
                // 可選：清空輸入欄位
                txtNTitle.Text = "";
                txtNGB.Text = "";
                txtCDate.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            }
            catch (Exception ex)
            {
                ShowAlert("留言失敗！\\n錯誤：" + ex.Message.Replace("'", ""));
            }

            


        }

        protected void btnB_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Page/GBS.aspx");
        }

        protected void CalCbtn_Click(object sender, ImageClickEventArgs e)
        {
            calN.Visible = true;
        }

        protected void calN_SelectionChanged(object sender, EventArgs e)
        {
            txtCDate.Text = calN.SelectedDate.ToString("yyyy/MM/dd");
            calN.Visible = false;
        }

        
    }
}