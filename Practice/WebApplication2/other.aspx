<%@ Page Language="C#" AutoEventWireup="true" CodeFile="other.aspx.cs" Inherits="WebApplication2.other" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>VS2010 彈窗（3 秒自動關閉 + IE7 判斷）</title>
    <meta charset="utf-8" />
        <link href="styles/site.css" rel="stylesheet" />
    <script type="text/javascript">
        //兩種測試方式目前的瀏覽器版本window.navigator.userAgent 或 document.documentMode
        if (document.documentMode <= 7) { 
            alert('IE相容性版本 (DocumentMode):'+ document.documentMode + ' IE');
        }
        else{
             alert('非IE模式（Chrome/Edge正常模式)');
        }
        
        var autoCloseTimer = null;
        var countdown = 5;

        //---------------------------------------
        //  IE 版本偵測
        //---------------------------------------
        function getIEVersion() {
            var ua = window.navigator.userAgent;
            var msie = ua.indexOf("MSIE ");

            // IE10 以下
            if (msie > -1) {
                return parseInt(ua.substring(msie + 5, ua.indexOf(".", msie)), 10);
            }

            // IE11
            if (ua.indexOf("Trident/") > -1) {
                return 11;
            }

            // 非 IE
            return -1;
        }

        //---------------------------------------
        //  顯示 modal + 自動倒數
        //---------------------------------------
        function showModal(title, message) {
            clearInterval(autoCloseTimer);
            countdown = 3;

            document.getElementById('modalTitle').innerText = title;
            document.getElementById('modalBody').innerText = message;
            document.getElementById('modalOverlay').style.display = 'flex';
            document.getElementById('closeTimer').innerText = "（"+ countdown +" 秒後自動關閉）";

            autoCloseTimer = setInterval(function () {
                countdown--;
                document.getElementById('closeTimer').innerText = "（" + countdown + " 秒後自動關閉）";

                if (countdown <= 0) {
                    hideModal();
                }
            }, 1000);
        }

        //---------------------------------------
        //  關閉 modal
        //---------------------------------------
        function hideModal() {
            clearInterval(autoCloseTimer);
            document.getElementById('modalOverlay').style.display = 'none';
        }

        //---------------------------------------
        //  統一彈窗（IE7 → alert）
        //---------------------------------------
        function showMessage(title, msg) {
            var ver = getIEVersion();

            if (ver == 7) {
                alert(title + "：\n" + msg);
            } else {
                showModal(title, msg);
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />

        <div style="padding:20px; font-family:Segoe UI, Tahoma, Arial;">
            <h2>VS2010 彈窗（5 秒自動關閉 + 確認鍵 + IE7 判斷）</h2>

            <input type="button"
                   class="btn"
                   value="客戶端彈窗測試"
                   onclick="showMessage('前端測試','這是前端呼叫的彈窗');" />

            <asp:Button ID="btnServer" runat="server" Text="伺服器端彈窗測試"
                        OnClick="btnServer_Click"
                        CssClass="btn" Style="margin-left:10px;" />
        </div>

        <!-- modal HTML -->
        <div id="modalOverlay" class="modal-overlay" hidden="hidden">
            <div class="modal" onclick="event.stopPropagation();">
                <div class="modal-header" id="modalTitle"></div>
                <div class="modal-body" id="modalBody"></div>
                <div class="modal-footer">
                    <span id="closeTimer"></span>
                    <input type="button" class="btn" value="確認" onclick="hideModal();" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
