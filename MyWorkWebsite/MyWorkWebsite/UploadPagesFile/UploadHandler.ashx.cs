using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Diagnostics;

namespace MyWorkWebsite.UploadPagesFile
{
    /// <summary>
    ///UploadHandler 的摘要描述
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
                    //Directory.CreateDirectory(parsedPath);
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
            // 如果輸入含檔名，移除最後的部分，只取目錄
            if (input.Contains("(") && input.Contains(")")) // 假設(檔案名稱).(副檔名)是範例，移除它
            {
                input = input.Substring(0, input.IndexOf('(')).Trim();
            }

            if (input.StartsWith("http://") || input.StartsWith("https://"))
            {
                Uri uri = new Uri(input);
                string host = uri.Host;
                string path = uri.AbsolutePath.TrimStart('/').Replace('/', '\\');
                return @"\\" + host + @"\c$\inetpub\wwwroot\shuhi\DataFolder" + path;
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