using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GuestBook
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["page"] == "home")
                {
                    contentFrame.Attributes["src"] = "Home.aspx";
                }
                else if (Request.QueryString["page"] == "query")
                {
                    contentFrame.Attributes["src"] = "/Page/GBS.aspx";
                }
            }
        }

        protected void lnkHPage_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx?page=home");
        }

        protected void lnkQPage_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx?page=query");
        }

        protected void lnksoPage_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

    }
}