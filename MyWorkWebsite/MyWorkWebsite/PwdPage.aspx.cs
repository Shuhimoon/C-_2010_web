using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls; 
// SQL lib 
using System.Data; 
using System.Data.SqlClient; 
//hash lib 
//BCryptHelper.cs 

namespace MyWorkWebsite {
    public partial class PwdPage : System.Web.UI.Page
    { 
        protected void Page_Load(object sender, EventArgs e) 
        { 
            int changePWD = Convert.ToInt32(Session["changePWD"]); 
            if (Session["UserName"] == null || changePWD == 1) 
            { 
                Response.Redirect("Login.aspx"); 
            } 
            
            if (!IsPostBack) 
            { 
                AccontInput.Text= Convert.ToString(Session["UserName"]); 
            } 
        } 
        
        protected void ReviseButton_Click(object sender, EventArgs e) 
        { 
            string newPassword = pwdInput.Text.Trim(); 
            string confirmPassword = confirmPwdInput.Text.Trim(); 
            if (string.IsNullOrWhiteSpace(newPassword)) 
            { 
                showMessenger.InnerText = "Please enter new password !"; 
                errorbox.Style["display"] = "block"; return; 
            } 
            if (newPassword != confirmPassword) 
            { 
                showMessenger.InnerText = "The passwords entered twice are inconsistent !"; 
                errorbox.Style["display"] = "block"; 
                return; 
            } 
            if (newPassword.Length < 8) 
            { 
                showMessenger.InnerText = "Password must be at least 8 characters long !"; 
                errorbox.Style["display"] = "block"; 
                return; 
            } 
            
            try {
                //用自己的套件= = 因為版本太舊破東西 BCryptHelper.cs 
                string passwordHash = BCryptHelper.HashPassword(newPassword); 
                
                string query = "UPDATE [User] Set Pwd = @password, changePWD = 1 WHERE UserName = @name"; 
                SqlParameter[] parameters = null; 
                parameters = new SqlParameter[] 
                { 
                    new SqlParameter("@name",Session["UserName"].ToString()), 
                    new SqlParameter("@password",passwordHash) 
                }; 

                int rows = Sql.ExecuteNonQuery(query, parameters); 
                
                if (rows > 0) 
                {
                    // 修改成功，清除強制改密碼旗標 
                    showMessenger.InnerText = "Password changed successfully ! Please log in again !"; 
                    errorbox.Style["display"] = "block"; 
                    
                    // 可選：強制登出
                    Session.Clear(); 
                    Response.Redirect("Login.aspx"); 
                } 
                else 
                {   
                    showMessenger.InnerText = "Modification failed, the user cannot be found ~"; 
                    errorbox.Style["display"] = "block"; 
                } 
            } 
            catch (Exception ex) 
            { 
                // 錯誤處理 
                lblMessage.Text = "Error: " + ex.Message; 
                lblMessage.Visible = true; 
            } 
        } 
        
        protected void checkBoxBtn_Click(object sender, EventArgs e) 
        {
            errorbox.Style["display"] = "none"; 
        } 
    } 
}