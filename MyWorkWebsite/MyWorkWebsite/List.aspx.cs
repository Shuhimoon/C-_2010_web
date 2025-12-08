using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyWorkWebsite
{
    public partial class List : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }


            //不要讓她一直抓著Post
            if (Request.QueryString["page"] == "home")
            {
                subPage.Attributes["src"] = "Home.aspx";
            }
            else if (Request.QueryString["page"] == "user")
            {
                subPage.Attributes["src"] = "UserChangePwd.aspx";
            }
        }
        protected void lnkHPage_Click(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx?page=home");
            
        }
        protected void lnkUserPage_Click(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx?page=user");
        }
        protected void logoutPage_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
            Session["UserName"] = null;
        }

    }
}