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
        private DataTable SearchGuestBook(DateTime start, DateTime end ,bool notitle)
        {
            DataTable dt = new DataTable();

            string sql = "";
            string titleKeyword = string.IsNullOrWhiteSpace(txtTitle.Text) ? "" : txtTitle.Text.ToString();



            if (!string.IsNullOrEmpty(titleKeyword) && notitle == false)
            {
                sql = "SELECT * FROM GusetBook " +
                      "WHERE Title LIKE @title";
                
            }
            else if (!string.IsNullOrEmpty(titleKeyword) && notitle == true)
            {
                sql = "SELECT * FROM GusetBook " +
                        "WHERE CreateDate >= @start " +
                        "AND CreateDate < DATEADD(day, 1, @end)" +
                        "AND Title LIKE @title";
            }
            else
            {
                sql = "SELECT * FROM GusetBook " +
                        "WHERE CreateDate >= @start " +
                        "AND CreateDate < DATEADD(day, 1, @end)";
            }



            using (SqlConnection conn = new SqlConnection(ConnStr))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                conn.Open(); 
                cmd.Parameters.AddWithValue("@start", start.Date);          
                cmd.Parameters.AddWithValue("@end", end.Date);
                if (!string.IsNullOrEmpty(titleKeyword))
                    cmd.Parameters.AddWithValue("@title", "%" + titleKeyword + "%");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }


        protected void btnS_Click(object sender, EventArgs e)
        {
            DateTime start, end;
           

            try
            {
                start = string.IsNullOrWhiteSpace(txtSDate.Text)
                    ? DateTime.Today
                    : DateTime.Parse(txtSDate.Text.Trim()).Date;
             
                end = string.IsNullOrEmpty(txtEDate.Text)
                    ? start
                    : DateTime.Parse(txtEDate.Text).Date;
            }
            catch
            {
                ltResult.Text = "<div class='reBox' style='color:red;'>日期格式錯誤！</div>";
                return;
            }

            // 3. 正確判斷：標題有沒有填
            bool notitle = !string.IsNullOrWhiteSpace(txtTitle.Text);

            // 查詢
            DataTable dt = SearchGuestBook(start, end, notitle);
            
            string html="";
            //判斷有沒有資料
            if (dt.Rows.Count == 0)
            {
                //如果沒有資料顯示你查詢的區段
                if (notitle) 
                    html += "<div class='reBox'> Title : "+ txtTitle.Text + "<br/>" + start.ToString("yyyy/MM/dd") + " 00:00:00 ~ " + end.ToString("yyyy/MM/dd") + " 23:59:59 <br/>查無此日期項目</div>";
                else
                    html += "<div class='reBox'> " + start.ToString("yyyy/MM/dd") + " 00:00:00 ~ " + end.ToString("yyyy/MM/dd") + " 23:59:59 <br/>查無此日期項目</div>";
                
            }
            else
            {
                //foreach 顯示結果
                foreach (DataRow row in dt.Rows)
                {
                    html += "<div class='rebox'>";
                    html += "<b> | ID : " + row["ID"].ToString() + " | </b>";
                    html += "<b> Title : " + row["Title"].ToString() + " | </b>";
                    html += "<b> Date : " + Convert.ToDateTime(row["CreateDate"]).ToString("yyyy/MM/dd") + " | </b>";
                    html += "<b>  GusetBook : " + row["GuestBook"].ToString() + " | </b>";
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
            Response.Redirect("/Page/GBU.aspx");
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

        


    }
}