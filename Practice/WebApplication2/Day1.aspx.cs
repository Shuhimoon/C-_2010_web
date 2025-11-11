using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class Day1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 若想在 Page_Load 時顯示（示範）
            // ScriptManager.RegisterStartupScript(this, this.GetType(), "startModal", "showModal('伺服器在 Page_Load', '這個訊息由伺服器在 Page_Load 時注入。');", true);
        }

        protected void btnServer_Click(object sender, EventArgs e)
        {
            string title = "伺服器端彈窗";
            string message = "這是由伺服器端按鈕觸發的訊息。";

            string script = string.Format("showMessage({0}, {1});",
                ToJsString(title), ToJsString(message));

            ScriptManager.RegisterStartupScript(
                this, this.GetType(),
                "serverPopup",
                script,
                true
            );
        }

        // 避免 JS 注入錯誤
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