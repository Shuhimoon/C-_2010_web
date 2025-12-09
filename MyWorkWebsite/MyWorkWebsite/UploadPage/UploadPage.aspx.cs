using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyWorkWebsite
{
    public partial class UploadPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void AddressConfirmButton_Click(object sender, EventArgs e)
        {

        }

        protected void AddressInput_TextChanged(object sender, EventArgs e)
        {

        }
    }
}