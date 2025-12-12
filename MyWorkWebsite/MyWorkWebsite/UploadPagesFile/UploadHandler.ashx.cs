using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Diagnostics;
using System.Configuration; // 新增這行來讀取設定檔


namespace MyWorkWebsite.UploadPagesFile
{
    /// <summary>
    /// UploadHandler 的摘要描述
    /// </summary>
    public class UploadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try
            {
                string address = context.Request.Form["address"];
                if (string.IsNullOrEmpty(address))
                {
                    context.Response.Write("無地址");
                    return;
                }

                // 解析地址
                string parsedPath = ParsePath(address);

                // 確保目錄存在
                if (!Directory.Exists(parsedPath))
                {
                    // Directory.CreateDirectory(parsedPath); // 如果想自動建立目錄，可以取消註解
                    context.Response.Write("目錄不存在: " + parsedPath);
                    return;
                }

                // 上傳檔案
                var files = context.Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    var file = files[i];
                    if (file.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(file.FileName); // 已編輯的檔名
                        string savePath = Path.Combine(parsedPath, fileName);
                        file.SaveAs(savePath);
                    }
                }

                context.Response.Write("上傳成功");
            }
            catch (Exception ex)
            {
                string logPath = @"E:\log\upload_log.txt"; // 自訂路徑
                File.AppendAllText(logPath, "錯誤: " + ex.Message + "\n" + ex.StackTrace + "\n");
                context.Response.Write("錯誤: " + ex.Message);
            }
        }

        private string ParsePath(string input)
        {
            // 如果輸入含檔名提示如 (檔案名稱).(副檔名)，移除它
            if (input.Contains("(") && input.Contains(")"))
            {
                input = input.Substring(0, input.IndexOf('(')).Trim();
            }

            if (input.StartsWith("http://") || input.StartsWith("https://"))
            {
                Uri uri = new Uri(input);
                string host = uri.Host; // 取出 IP 或 host
                string pathRelative = uri.AbsolutePath.TrimStart('/').Replace('/', '\\'); // 相對路徑，如 File_One\File_Two\ 或 List.aspx

                // 如果有副檔名，取目錄部分 (忽略檔名)
                string extension = Path.GetExtension(pathRelative);
                if (!string.IsNullOrEmpty(extension))
                {
                    pathRelative = Path.GetDirectoryName(pathRelative) ?? "";
                }
                // 從設定檔讀取基路徑後綴 (web.config)
                string UploadPageDir = ConfigurationManager.AppSettings["uploadpagedir"] ?? @"\c$\inetpub\wwwroot\shuhi\Filefolder";

                // 如果設定檔沒值，用預設
                string basePath = @"\\" + host + UploadPageDir;

                // 組合路徑 (自動處理 \ 分隔)
                return Path.Combine(basePath, pathRelative);
            }
            else
            {
                // 本地路徑，確保是有效盤符
                return input.Replace('/', '\\');
            }
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}