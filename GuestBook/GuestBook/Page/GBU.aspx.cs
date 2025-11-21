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
    public partial class GBU : System.Web.UI.Page
    {
        //建立連線字串
        private string ConnStr = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
        private DataTable SearchGuestBook(string itemid)
        {
            DataTable dt = new DataTable();
           

            //讀取login 是用哪個使用者登入的 ，去讀取Session["UserID"]
            int userId = Convert.ToInt32(Session["UserID"]);

            string sql = "SELECT * FROM GusetBook " +
                      "WHERE ID = @ID" +
                      " AND User_ID = @UserID";

            using (SqlConnection conn = new SqlConnection(ConnStr))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@ID", itemid);
                cmd.Parameters.AddWithValue("@UserID", userId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        private void UpdateGuestBook(string itemid,string title,string gbook)
        {
            string sql = @"UPDATE GusetBook
                           SET Title=@title, 
                               GuestBook=@gbook, 
                               UpdateDate=GETDATE()
                           WHERE ID = @ID";

            using (SqlConnection conn = new SqlConnection(ConnStr))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@ID", itemid);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@gbook", gbook);
                

                int rows = cmd.ExecuteNonQuery();  // 執行更新

                    // 檢查有沒有真的更新到資料
                if (rows == 0)
                    throw new Exception("更新失敗，可能資料已被刪除或無權限");
                
                
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
                string itemID = Request.QueryString["itemID"];
                if (!string.IsNullOrEmpty(itemID))
                {
                    Session["itemID"] = itemID;
                    // 執行後續邏輯，例如顯示刪除確認頁面
                    DataTable dt = SearchGuestBook(itemID);
                    foreach (DataRow row in dt.Rows)
                    {
                      
                        txtR.Text=row["Title"].ToString();
                        if (row["UpdateDate"] != DBNull.Value)
                        {
                            txtUR.Text = Convert.ToDateTime(row["UpdateDate"]).ToString("yyyy/MM/dd HH:mm:ss");
                        }
                        else
                        {
                            txtUR.Text = "-";  // 或顯示「無資料」、「-」之類的
                        }
                        txtRGB.Text = row["GuestBook"].ToString();
                       
                    }
                }
                else
                {
                    Response.Redirect("/Page/GBS.aspx");
                }

                if (Session["UserID"] == null)
                {
                    Response.Redirect("~/Login.aspx"); // 沒登入就踢回去登入頁
                }

                
            }
        }

        protected void btnUpdateConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateGuestBook(Convert.ToString(Session["itemID"]), txtR.Text, txtRGB.Text);

                // 清空輸入欄位
                txtR.Text = "";
                txtRGB.Text = "";
                Response.Redirect("/Page/GBS.aspx");

            }
            catch (Exception ex)
            {
                //要換一下
                ShowAlert("留言失敗！\\n錯誤：" + ex.Message.Replace("'", ""));
            }

        }

        protected void btnB_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Page/GBS.aspx");
        }
    }
}