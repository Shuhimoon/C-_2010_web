<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Day1.aspx.cs" Inherits="WebApplication2.Day1" %>

<asp:Content ID="Content_HeadDay1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta charset="utf-8" />
    <link href="styles/site.css" rel="stylesheet" />

    <script type="text/javascript">
        // IE 版本偵測
        function getIEVersion() {
            var ua = navigator.userAgent;
            var msie = ua.indexOf("MSIE ");
            if (msie > -1) return parseInt(ua.substring(msie + 5, ua.indexOf(".", msie)), 10);
            if (ua.indexOf("Trident/") > -1) return 11;
            return -1;
        }

        // modal 顯示
        function showModal(title, message) {
            var overlay = document.getElementById('modalOverlay');
            document.getElementById('modalTitle').innerText = title || '訊息';
            document.getElementById('modalBody').innerText = message || '';
            overlay.style.display = 'flex';
        }
        function hideModal() {
            document.getElementById('modalOverlay').style.display = 'none';
        }

        // 自動判斷 IE7 或新瀏覽器
        function showMessage(title, msg) {
            var ver = getIEVersion();
            if (ver <= 7 && ver > 0) {
                alert(title + "：\n" + msg);
            } else {
                showModal(title, msg);
            }
        }
    </script>
</asp:Content>


<asp:Content ID="Content_Day1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <div style="padding:20px; font-family:Segoe UI, Tahoma, Arial;">
        <h2>VS2010 彈窗（自動判斷 IE7 / 新瀏覽器）</h2>

        <input type="button" class="btn" value="客戶端彈窗測試"
               onclick="showMessage('客戶端測試','這是前端呼叫的彈窗');" />

        <asp:Button ID="btnServer" runat="server" Text="伺服器端彈窗測試"
                    OnClick="btnServer_Click" CssClass="btn" Style="margin-left:10px;" />
    </div>

    <!-- modal -->
    <div id="modalOverlay" class="modal-overlay" onclick="hideModal();" style="display:none;">
        <div class="modal" onclick="event.stopPropagation();">
            <div class="modal-header" id="modalTitle">標題</div>
            <div class="modal-body" id="modalBody">內容</div>
            <div class="modal-footer">
                <input type="button" class="btn" value="關閉" onclick="hideModal();" />
            </div>
        </div>
    </div>

</asp:Content>
