<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GBU.aspx.cs" Inherits="GuestBook.GBU" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Revise</title>
    <link href="../styles/gb.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="Container">
            <h2>Revise Guest Book</h2> 
                    <span class="title">Title</span>
                    <asp:TextBox ID="txtR" CssClass="input" runat="server"></asp:TextBox><br><br/>
                    <span class="title">更新日期</span>
                    <asp:TextBox ID="txtUR" CssClass="input" Enabled="false" runat="server"></asp:TextBox>


                <br><br/>
                <span class="title">Guest Book</span>
                <div class="TBoxRow">
                   <asp:TextBox ID="txtRGB" CssClass="inputGB" TextMode="MultiLine" Rows="20" runat="server"></asp:TextBox>
                </div>

                <div class="BtRow">
                    <asp:Button ID="btnR" CssClass="btn" runat="server" Text="確認修改" OnClientClick="showBox(); return false;" />
                    <asp:Button ID="btnB" CssClass="btn" runat="server" Text="返回" Onclick="btnB_Click" />
                </div>

                 <!-- 刪除確認Box -->
                 <div ID="deleteOverlay">
                        <div ID="deleteOverlay-box">
                            <h3>確認修改</h3>
                            <p>確定要修改這筆資料嗎？此動作無法復原！</p>
        
                            <!-- 隱藏的真刪除按鈕（會真正觸發後端刪除） -->
                            <asp:Button ID="btnUpdateConfirm" runat="server" Text="確認修改" CssClass="btn" OnClick="btnUpdateConfirm_Click" UseSubmitBehavior="false" />
                            <button type="button" class="btn" onclick="hideBox()">取消</button>
                        </div>
                </div>
                <script type="text/javascript">
                    function showBox() {
                        document.getElementById('deleteOverlay').style.display = 'block';
                    }
                    function hideBox() {
                        document.getElementById('deleteOverlay').style.display = 'none';
                    }

                    // 如果使用者按了 ESC 鍵也關閉 ， IE7 不能用先關掉
                    //document.onkeydown = function (e) {
                    //    if (e.key === "Escape") hideDeleteBox();
                    //};
                </script>

        </div>
    </form>
</body>
</html>
