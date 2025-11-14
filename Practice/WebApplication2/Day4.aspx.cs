using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebApplication2
{
    public partial class Day4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private DataTable GetData()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name",typeof(string));
            dt.Columns.Add("Price", typeof(int));

            dt.Rows.Add(1,"Apple",40);
            dt.Rows.Add(2, "banana", 50);
            dt.Rows.Add(3, "watermelon", 100);

            return dt;
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            gvData.DataSource = GetData(); //將你設定好的表格丟進去
            gvData.DataBind(); //顯示表格
        }

        protected void gvData_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            //如果是show就執行
            if (e.CommandName == "Show")
            {
                //
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvData.Rows[rowIndex];

                
                string id = row.Cells[0].Text;
                string name = row.Cells[1].Text;
                string price = row.Cells[2].Text;

                //點選按鈕顯示的內容
                lblResult.Text = "You click id :  " + id + "<br />Name: " + name + "<br />Price：" + price;
            }
        }
    }
}