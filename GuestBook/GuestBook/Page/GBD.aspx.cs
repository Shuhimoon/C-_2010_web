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
    public partial class GBD : System.Web.UI.Page
    {

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

        private void DeleteGuestBook(string itemid)
        {
            string sql = @"Delete FROM GusetBook WHERE ID = @ID";

            using (SqlConnection conn = new SqlConnection(ConnStr))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@ID", itemid);
                

                int rows = cmd.ExecuteNonQuery();  // 執行更新

                // 檢查有沒有真的更新到資料
                if (rows == 0)
                    throw new Exception("刪除失敗，可能資料已被刪除或無權限");

            }

        }


        protected void Page_Load(object sender, EventArgs e)
        {
            string itemID = Request.QueryString["itemID"];
            if (!string.IsNullOrEmpty(itemID))
            {
                Session["itemID"] = itemID;
                // 執行後續邏輯，例如顯示刪除確認頁面
                DataTable dt = SearchGuestBook(itemID);
                foreach (DataRow row in dt.Rows)
                {

                    txtD.Text = row["Title"].ToString();
                    txtCD.Text = Convert.ToDateTime(row["CreateDate"]).ToString("yyyy/MM/dd HH:mm:ss");
                    if (row["UpdateDate"] != DBNull.Value)
                    {
                        txtDD.Text = Convert.ToDateTime(row["UpdateDate"]).ToString("yyyy/MM/dd HH:mm:ss");
                    }
                    else
                    {
                        txtDD.Text = "-";  // 或顯示「無資料」、「-」之類的
                    }
                    txtDGB.Text = row["GuestBook"].ToString();

                }
            }
            else
            {
                Response.Redirect("~/GBS.aspx");
            }
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Login.aspx"); // 沒登入就踢回去登入頁
            }

        }

        protected void btnD_Click(object sender, EventArgs e)
        {

        }

        protected void btnB_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Page/GBS.aspx");
        }

        protected void btnDeleteConfirm_Click(object sender, EventArgs e)
        {
            DeleteGuestBook(Convert.ToString(Session["itemID"]));
            Response.Redirect("/Page/GBS.aspx");
        }
    }
}