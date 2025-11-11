using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class Day3 : System.Web.UI.Page
    {
        private void AppendLog(string text)
        {
            lblLog.Text += text + "<br />";
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            AppendLog("Page_Init<br />");
        }

        //使用 HiddenField 或 Session ，不要使用ViewState 他會造成使用者網頁卡頓

        /*protected void Page_Load(object sender, EventArgs e)
        {
            AppendLog("Page_Load，IsPostBack=" + IsPostBack + "<br />");
            if (!IsPostBack) {
                ViewState["Count"] = 0;
                lblCount.Text = "Total: 0<br />";
            }
        }
        protected void btnCount_Click(object sender, EventArgs e)
        {
            int count = (int)ViewState["Count"];
            count++;

            ViewState["Count"] = count;
            lblCount.Text = "Total:" + count + "<br />";
            AppendLog("btnCount_Click<br />");
        }*/

        protected void Page_Load(object sender, EventArgs e)
        {
            AppendLog("Page_Load，IsPostBack=" + IsPostBack + "<br />");
            if (!IsPostBack)
            {
                hidCount.Value = "0";
                lblCount.Text = "Total: 0<br />";
            }
        }
        protected void btnCount_Click(object sender, EventArgs e)
        {
            int count = int.Parse(hidCount.Value);
            count++;

            hidCount.Value = count.ToString();
            lblCount.Text = "Total: " + count + "<br />";

        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            AppendLog("Page_PreRender<br />");
        }
        
    }
}