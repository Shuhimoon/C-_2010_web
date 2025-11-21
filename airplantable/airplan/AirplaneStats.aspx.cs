using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Collections.Generic;
using System.Text;

namespace airplan
{
    public partial class AirplaneStats : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        private void BindGrid()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MYDB"].ConnectionString;

            // 動態生成年/月：使用 for 迴圈控制年份與月份範圍（例如 2025 年 1-12 月；可調整 startYear, endYear, startMonth, endMonth 為靈活參數）
            // 若需多個年份，可設定 startYear = 2025, endYear = 2026 等
            int startYear = 2025;  // 起始年份（可改為變數或從使用者輸入取得）
            int endYear = 2025;    // 結束年份
            int startMonth = 1;    // 起始月份
            int endMonth = 12;     // 結束月份

            List<string> periods = new List<string>();
            for (int year = startYear; year <= endYear; year++)
            {
                for (int month = startMonth; month <= endMonth; month++)
                {
                    periods.Add(year.ToString() + "/" + month.ToString("00"));  // 生成如 2025/11
                }
            }

            // 建構 PIVOT 的欄位字串：ISNULL([2025/11], 0) AS [2025/11], ...
            StringBuilder columnsBuilder = new StringBuilder();
            foreach (string period in periods)
            {
                if (columnsBuilder.Length > 0) columnsBuilder.Append(", ");
                columnsBuilder.Append("ISNULL([" + period + "], 0) AS [" + period + "]");
            }

            // 建構 PIVOT IN 子句：[2025/11], [2025/12], ...
            List<string> pivotInList = new List<string>();
            foreach (string p in periods)
            {
                pivotInList.Add("[" + p + "]");
            }
            string pivotIn = string.Join(", ", pivotInList.ToArray());

            // 動態 SQL：使用 PIVOT 將月份轉為欄位，統計每 User_ID 的記錄數（無資料顯示 0）
            // 使用 .NET 4 相容語法：避免 $ 字串插值，使用 + 連接字串；移除 SQL 內 -- 註解（因單行字串會註解掉後續，導致語法錯誤 near 'GusetBook'）
            string query = "SELECT User_ID, " + columnsBuilder.ToString() +
                           " FROM " +
                           " ( " +
                           "     SELECT User_ID, " +
                           "            CAST(YEAR(CreateDate) AS varchar(4)) + '/' + " +
                           "            RIGHT('0' + CAST(MONTH(CreateDate) AS varchar(2)), 2) AS M " +
                           "     FROM [GusetBook] " +  // 使用您指定的表格名稱 [GusetBook]
                           "     WHERE CreateDate >= '2025-01-01' " +
                           " ) AS A " +
                           " PIVOT " +
                           " ( " +
                           "     COUNT(M) " +
                           "     FOR M IN (" + pivotIn + ") " +
                           " ) AS P " +
                           " ORDER BY User_ID;";

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // 除錯輸出：顯示實際 SQL 查詢與錯誤訊息（在頁面顯示，便於診斷）
                Response.Write("SQL Query for Debug: <pre>" + query + "</pre><br />");
                Response.Write("Error Message: " + ex.Message + "<br />");
                Response.Write("Stack Trace: " + ex.StackTrace);
            }
        }
    }

}