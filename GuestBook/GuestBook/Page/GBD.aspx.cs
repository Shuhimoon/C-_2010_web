using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GuestBook
{
    public partial class GBD : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnD_Click(object sender, EventArgs e)
        {

        }

        protected void btnB_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Page/GBS.aspx");
        }
    }
}