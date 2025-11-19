<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GuestBook.Default" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home</title>
    <link href="styles/default.css" rel="stylesheet"/>
</head>
<body>
    <form id="DfPage" runat="server">
     <!--topMenu-->
    <div class="tMenu">
        <asp:LinkButton ID="HPage" CssClass="link" runat="server" OnClick="lnkHPage_Click">Home</asp:LinkButton>
        <asp:LinkButton ID="QPage" CssClass="link" runat="server" OnClick="lnkQPage_Click">Query</asp:LinkButton>

        
        <asp:LinkButton ID="SingOut" CssClass="solink" runat="server" OnClick="lnksoPage_Click">[Sing Out]</asp:LinkButton>
        <span id="userName" class="userText" runat="server"></span>
        <img src="head.png" alt="image" class="imagestyle">
    </div>

    <!--子頁面內容 讓他去後端調用src-->
    <div class="subpContainer">
        <iframe id="contentFrame" name="contentFrame"
                src="Home.aspx"
                runat="server"
                frameborder="0"
                allowtransparency="true">
        </iframe>
    </div>
    </form>

    <script type="text/javascript">
        function loadPage(url) {
            document.getElementById('contentFrame').src = url;
        }
        
    </script>
</body>
</html>
