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
    public partial class GBS : System.Web.UI.Page
    {
        //建立連線字串
        private string ConnStr = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
        private DataTable SearchGuestBook(DateTime start, DateTime end ,bool notitle,bool dtimecheck)
        {
            DataTable dt = new DataTable();

            string sql = "";
            string titleKeyword = string.IsNullOrWhiteSpace(txtTitle.Text) ? "" : txtTitle.Text.ToString();

            //讀取login 是用哪個使用者登入的 ，去讀取Session["UserID"]
            int userId = Convert.ToInt32(Session["UserID"]);

            
            //有標題 沒日期
            if (notitle == false && dtimecheck == true)
            {
                sql = "SELECT * FROM GusetBook " +
                      "WHERE Title LIKE @title" +
                      " AND User_ID = @UserID";
                
            }//有標題 有日期
            else if (notitle == false && dtimecheck == false)
            {
                sql = "SELECT * FROM GusetBook " +
                        "WHERE CreateDate >= @start " +
                        " AND CreateDate < DATEADD(day, 1, @end)" +
                        " AND Title LIKE @title" +
                        " AND User_ID = @UserID";
            }
            else//沒標題 沒日期 
            {
                sql = "SELECT * FROM GusetBook " +
                        "WHERE CreateDate >= @start " +
                        " AND CreateDate < DATEADD(day, 1, @end)" +
                        " AND User_ID = @UserID";
            }



            using (SqlConnection conn = new SqlConnection(ConnStr))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                conn.Open(); 
                cmd.Parameters.AddWithValue("@start", start.Date);          
                cmd.Parameters.AddWithValue("@end", end.Date);
                cmd.Parameters.AddWithValue("@title", "%" + titleKeyword + "%");
                cmd.Parameters.AddWithValue("@UserID", userId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Login.aspx"); // 沒登入就踢回去登入頁
            }
        }


        protected void btnS_Click(object sender, EventArgs e)
        {
            DateTime start, end;
            bool dtimecheck = false;

            if (string.IsNullOrEmpty(txtSDate.Text))
            {
                start = DateTime.Today;
                dtimecheck = true;
            }
            else
            {
                start = DateTime.Parse(txtSDate.Text.Trim()).Date;
                dtimecheck = false;
            }

            if (string.IsNullOrEmpty(txtEDate.Text))
            {
                end = start;
            }
            else
            {
                if (dtimecheck == true)
                {
                    end = DateTime.Parse(txtEDate.Text).Date;
                    start = end;
                    dtimecheck = false;
                }
                else
                    end = DateTime.Parse(txtEDate.Text).Date;
            }

            if (start > end)
            {
                DateTime temp = start;
                start = end;
                end = temp;
            }

            // 3. 正確判斷：標題有沒有 ， 有的話為true
            bool notitle = string.IsNullOrWhiteSpace(txtTitle.Text);

            // 查詢
            DataTable dt = SearchGuestBook(start, end, notitle, dtimecheck);
            
            string html="";
            

            //判斷有沒有資料
            if (dt.Rows.Count == 0)
            {
                //有title ， 沒有 time
                if (notitle == false && dtimecheck == true)
                    html += "<div class='reBox'> Title : " + HttpUtility.HtmlEncode(txtTitle.Text.ToString()) + " 查無此日期項目</div>";
                else if (notitle == false && dtimecheck == false) //有title ， 有 time
                    html += "<div class='reBox'> Title : " + HttpUtility.HtmlEncode(txtTitle.Text.ToString()) + "<br/>" + start.ToString("yyyy/MM/dd") + " 00:00:00 ~ " + end.ToString("yyyy/MM/dd") + " 23:59:59 <br/>查無此日期項目</div>";
                else
                    html += "<div class='reBox'> " + start.ToString("yyyy/MM/dd") + " 00:00:00 ~ " + end.ToString("yyyy/MM/dd") + " 23:59:59 <br/>查無此日期項目</div>";
                
            }
            else
            {
                //foreach 顯示結果
                foreach (DataRow row in dt.Rows)
                {
                    
                    html += "<div class='rebox'>";
                    html += "<b> | ID : <a href='/Page/GBD.aspx?itemID=" + row["ID"].ToString() + "'>" + row["ID"].ToString() + "</a> | </b>"; 
                    html += "<b> Title : " + HttpUtility.HtmlEncode(row["Title"].ToString()) + " | </b>";
                    html += "<b> Date : " + Convert.ToDateTime(row["CreateDate"]).ToString("yyyy/MM/dd") +" | </b>";
                    html += "<b> GusetBook : " + HttpUtility.HtmlEncode(row["GuestBook"].ToString()) +" | </b>";
                    html += "<br/><br/></div>";
                }
            }
           
            //輸出至前端
            ltResult.Text = html;
        }

        protected void btnN_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Page/GBN.aspx");
        }

        protected void btnU_Click(object sender, EventArgs e)
        {
            DateTime start, end;
            bool dtimecheck = false;



            if (string.IsNullOrEmpty(txtSDate.Text))
            {
                start = DateTime.Today;
                dtimecheck = true;
            }
            else
            {
                start = DateTime.Parse(txtSDate.Text.Trim()).Date;
                dtimecheck = false;
            }

            if (string.IsNullOrEmpty(txtEDate.Text))
            {
                end = start;
            }
            else
            {
                if (dtimecheck == true)
                {
                    end = DateTime.Parse(txtEDate.Text).Date;
                    start = end;
                    dtimecheck = false;
                }
                else
                    end = DateTime.Parse(txtEDate.Text).Date;
            }

            if (start > end)
            {
                DateTime temp = start;
                start = end;
                end = temp;
            }

            // 3. 正確判斷：標題有沒有 ， 有的話為true
            bool notitle = string.IsNullOrWhiteSpace(txtTitle.Text);

            // 查詢
            DataTable dt = SearchGuestBook(start, end, notitle, dtimecheck);

            string html = "";



            //判斷有沒有資料
            if (dt.Rows.Count == 0)
            {
                //有title ， 沒有 time
                if (notitle == false && dtimecheck == true)
                    html += "<div class='reBox'> Title : " + HttpUtility.HtmlEncode(txtTitle.Text.ToString()) + " 查無此日期項目</div>";
                else if (notitle == false && dtimecheck == false) //有title ， 有 time
                    html += "<div class='reBox'> Title : " + HttpUtility.HtmlEncode(txtTitle.Text.ToString()) + "<br/>" + start.ToString("yyyy/MM/dd") + " 00:00:00 ~ " + end.ToString("yyyy/MM/dd") + " 23:59:59 <br/>查無此日期項目</div>";
                else
                    html += "<div class='reBox'> " + start.ToString("yyyy/MM/dd") + " 00:00:00 ~ " + end.ToString("yyyy/MM/dd") + " 23:59:59 <br/>查無此日期項目</div>";

            }
            else
            {
                //foreach 顯示結果
                foreach (DataRow row in dt.Rows)
                {

                    html += "<div class='rebox'>";
                    html += "<b> | ID : <a href='/Page/GBU.aspx?itemID=" + row["ID"].ToString() + "'>" + row["ID"].ToString() + "</a> | </b>";
                    html += "<b> Title : " + HttpUtility.HtmlEncode(row["Title"].ToString()) + " | </b>";
                    html += "<b> Date : " + Convert.ToDateTime(row["CreateDate"]).ToString("yyyy/MM/dd") + " | </b>";
                    html += "<b> GusetBook : " + HttpUtility.HtmlEncode(row["GuestBook"].ToString()) + " | </b>";
                    html += "<br/><br/></div>";
                }
            }

            //輸出至前端
            ltResult.Text = html;
        }

        

        protected void calS_SelectionChanged(object sender, EventArgs e)
        {
            txtSDate.Text = calS.SelectedDate.ToString("yyyy/MM/dd");
            calS.Visible = false;

        }

        protected void calE_SelectionChanged(object sender, EventArgs e)
        {
            txtEDate.Text = calE.SelectedDate.ToString("yyyy/MM/dd");
            calE.Visible = false;
        }

        protected void CalSbtn_Click(object sender, EventArgs e)
        {
            calS.Visible = true;
        }
        protected void CalEbtn_Click(object sender, EventArgs e)
        {
            calE.Visible = true;
        }

        protected void txtSDate_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtEDate_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtTitle_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnDeleteConfirm_Click(object sender, EventArgs e)
        {

        }

             


    }
}