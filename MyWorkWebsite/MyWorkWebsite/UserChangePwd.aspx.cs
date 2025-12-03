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
    public partial class UserChangePwd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }
            if (!IsPostBack)
            {
                nameSelect.Text = "";  // 首次清除
                BindUserList();  // 只首次綁定
                lblMessage.Visible = false;  // 初始隱藏
                errorbox.Style["display"] = "none";  // 初始隱藏 errorbox（假設預設不顯示）
            }
        }

        private void BindUserList()
        {
            try
            {
                string query = "SELECT ID, UserName FROM [User]";  // 調整欄位如果不同
                SqlParameter[] parameters = null;  // 無參數
                               //這邊的Sql是我的模組命名
                DataTable dt = Sql.ExecuteQuery(query, parameters);  // 使用你的 Sql 類別；如果改成 DbHelper，請調整

                rptUserList.DataSource = dt;
                rptUserList.DataBind();
            }
            catch (Exception ex)
            {
                // 錯誤處理，例如顯示訊息（新增 asp:Label ID="lblError"）
                lblMessage.Text = "Error: " + ex.Message;
            }
        }

        protected void rptUserList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();

            switch (e.CommandName)
            {
                case "RemakeEntry":
                    // 重製
                    try
                    {
                        string query = "UPDATE [User] SET Pwd = '0000', Enable = 1, changePWD = 0 WHERE ID = @id";
                        SqlParameter[] parameters = new SqlParameter[]
                        {
                            new SqlParameter("@id", Convert.ToInt32(id))
                        };

                        int rowsAffected = Sql.ExecuteNonQuery(query, parameters);  // 假設 Sql 有 ExecuteNonQuery 方法
                        if (rowsAffected > 0)
                        {
                            BindUserList();  // 重新綁定列表
                        }
                        showMessenger.InnerText = "Reproduced successfully !";
                        errorbox.Style["display"] = "block";

                    }
                    catch (Exception ex)
                    {
                        // 錯誤處理
                        lblMessage.Text = "Error: " + ex.Message;
                    }
                    break;
                case "DeleteEntry":
                    // 刪除邏輯
                    try
                    {
                        string query = "DELETE FROM [User] WHERE ID = @id";
                        SqlParameter[] parameters = new SqlParameter[]
                        {
                            new SqlParameter("@id", Convert.ToInt32(id))
                        };

                        int rowsAffected = Sql.ExecuteNonQuery(query, parameters);  // 假設 Sql 有 ExecuteNonQuery 方法
                        if (rowsAffected > 0)
                        {
                            BindUserList();  // 重新綁定列表
                        }
                        showMessenger.InnerText = "Delete successfully !";
                        errorbox.Style["display"] = "block";

                    }
                    catch (Exception ex)
                    {
                        // 錯誤處理
                        lblMessage.Text = "Error: " + ex.Message;
                    }
                    break;
            }
        }

        protected void selectBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string query ="";
                SqlParameter[] parameters = null;
                if (!string.IsNullOrWhiteSpace(nameSelect.Text))//不是 null、空字串、也不是只有空白或換行
                {
                    parameters = new SqlParameter[]
                    {
                        new SqlParameter("@name" , nameSelect.Text.Trim())
                    };
                    query = "SELECT ID, UserName FROM [User] WHERE UserName = @name";
                }
                else
                {
                    parameters = null;
                    query = "SELECT ID, UserName FROM [User]";
                }

                DataTable dt = Sql.ExecuteQuery(query, parameters);
                if (dt.Rows.Count == 0)
                {
                    showMessenger.InnerText = string.Format("There is no such {0} user !", nameSelect.Text.Trim());
                    errorbox.Style["display"] = "block";
                    nameSelect.Text = "";
                }
                else
                {
                    rptUserList.DataSource = dt;
                    rptUserList.DataBind(); 
                }
            }
            catch (Exception ex)
            {
                // 錯誤處理
                lblMessage.Text = "Error: " + ex.Message;
            }
            
        }

        protected void checkBoxBtn_Click(object sender, EventArgs e)
        {
            errorbox.Style["display"] = "none";
        }
    }
}