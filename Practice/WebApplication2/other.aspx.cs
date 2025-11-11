using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class other : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnServer_Click(object sender, EventArgs e)
        {
            string title = "伺服器端彈窗";
            string message = "這是由伺服器端按下按鈕後顯示的彈窗。";

            string script = string.Format("showMessage({0}, {1});",
                ToJsString(title), ToJsString(message));

            ScriptManager.RegisterStartupScript(
                this, this.GetType(),
                "serverPopup",
                script,
                true
            );
        }

        private string ToJsString(string s)
        {
            if (s == null) return "''";
            return "'" + s.Replace("\\", "\\\\")
                          .Replace("'", "\\'")
                          .Replace("\r", "\\r")
                          .Replace("\n", "\\n") + "'";
        }
    }
}