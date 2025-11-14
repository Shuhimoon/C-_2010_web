<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GBS.aspx.cs" Inherits="GuestBook.GBS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Query</title>
    <link href="../styles/gb.css" rel="stylesheet"/>
</head>
<body>
    <form id="GBSPage" runat="server">
        <div class="Container">
            <h2>Query System</h2>     
            <div class="TBoxRow">
                <span class="title">Title</span>
                <asp:TextBox ID="txtSearch" CssClass="input" runat="server"></asp:TextBox>
            </div>

            <div class="BtRow">
                <asp:Button ID="btnS" CssClass="btn" runat="server" Text="搜尋" OnClick="btnS_Click" />
                <asp:Button ID="btnN" CssClass="btn" runat="server" Text="新增" Onclick="btnN_Click" />
                <asp:Button ID="btnU" CssClass="btn" runat="server" Text="修改" Onclick="btnU_Click" />
            </div>

            <!--顯示內容-->
            <br><br/>
            

        </div>
    </form>
</body>
</html>
