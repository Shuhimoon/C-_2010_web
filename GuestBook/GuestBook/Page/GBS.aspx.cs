using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GuestBook
{
    public partial class GBS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnS_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnN_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Page/GBN.aspx");
        }

        protected void btnU_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Page/GBU.aspx");
        }
    }
}