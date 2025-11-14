using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GuestBook.Styles
{
    public partial class GBN : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)   // 避免重新整理又重跑
            {
                txtND.Text = DateTime.Now.ToString("yyyy/MM/dd  HH:mm:ss");
            }
        }

        protected void btnC_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnB_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Page/GBS.aspx");
        }

        
    }
}