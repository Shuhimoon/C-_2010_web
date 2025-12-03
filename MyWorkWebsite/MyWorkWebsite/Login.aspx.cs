using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// SQL lib
using System.Data;
using System.Data.SqlClient;

namespace MyWorkWebsite
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AccontInput.Text = "";
                pwdInput.Text = "";
                errorbox.Style["display"] = "none";

                // 檢查是否為新 Session（第一次加載頁面或瀏覽器新開）
                if (Session.IsNewSession)
                {
                    Session["UserName"] = null;  // 明確設為 null
                }
                else
                {
                    // 之後再說
                    // 如果不是新 Session，但用戶尚未登出（假設你有個登入旗標，例如 Session["IsLoggedIn"]）
                    // 你可以根據你的登入邏輯調整條件，例如如果未登入則清除
                    if (Session["IsLoggedIn"] == null || !(bool)Session["IsLoggedIn"])
                    {
                        Session["UserName"] = null;  // 在未登出但無登入狀態時設為 null
                    }
                }



                //禁止快取帳號密碼
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetNoStore();
                
            }
            if (Session["UserName"] != null)
            {
                Response.Redirect("List.aspx");
                return;
            }
            
        }

        protected void lgButton_Click(object sender, EventArgs e)
        {
            try {
                //不小心用到保留字眼
                string query = "select * From [User] Where UserName=@name and Pwd=@pwd";

                string pwdhash = BCryptHelper.HashPassword(pwdInput.Text.Trim());

                lblMessage.Text = pwdhash;
                //設定查詢參數
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@name" , AccontInput.Text.Trim()),
                    //new SqlParameter("@pwd" , pwdInput.Text.Trim())
                    new SqlParameter("@pwd" , pwdhash)
                };

                DataTable result = Sql.ExecuteQuery(query, parameters);

                if (result.Rows.Count == 1)
                {
                    
                    Session["UserName"] = AccontInput.Text.Trim();
                    Session["PwdStateCheck"] = Convert.ToInt32(result.Rows[0]["changePWD"]);
                    if (Convert.ToInt32(result.Rows[0]["changePWD"]) == 1)
                    {
                        Response.Redirect("List.aspx");
                    }
                    else
                    {
                        Response.Redirect("PwdPage.aspx");
                    }
                    

                }
                else
                {
                    errorbox.Style["display"] = "block";
                }
                
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;  // 捕捉如連接錯誤
            }


        }

        protected void checkBoxBtn_Click(object sender, EventArgs e)
        {
            AccontInput.Text = "";
            pwdInput.Text = "";
            errorbox.Style["display"] = "none";
            Response.Redirect("Login.aspx");
        }
    }
}