<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="GuestBook.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> Login </title>
    <link href="styles/site.css" rel="stylesheet"/>
</head>
<body>
    <form id="lgPage" runat="server">
        <div class="Container">
            <h2>Login</h2>
            <div class="TBoxRow">
                <!--span 不會換行-->
                <span>帳號</span>
                <asp:TextBox ID="AcountInput" CssClass="input" runat="server"/>
            </div>
            <div class="TBoxRow">
                <!--span 不會換行-->
                <span>密碼</span>
                <asp:TextBox ID="PWD" type="password" CssClass="input" runat="server"/>
            </div>
            <div class="BtRow">
                <asp:Button ID="lgBt" CssClass="btn" runat="server" Text="登入" onclick="lgBt_Click"/>
            </div>

            <asp:Literal ID="ltResult" runat="server" Visible="false" Text="123"></asp:Literal>
        </div>
    </form>
</body>
</html>
