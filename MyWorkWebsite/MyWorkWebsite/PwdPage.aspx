<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PwdPage.aspx.cs" Inherits="MyWorkWebsite.PwdPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PWD</title>
    <link href="Styles/Default.css" rel="Stylesheet"/>
    <!--[if IE]><link href="IE_Styles/IEDefault.css" type="text/css" rel="Stylesheet"/><![endif]-->
</head>
<body>
    <form id="form1" runat="server">
        <h1 class="bk">Shuhi Website</h1>
        <h1>Shuhi Website</h1>
        <div class="Container">
            <h2>Change Password</h2>
            <div class="ContainerSub">
                <span>Accont：</span>
                <asp:TextBox ID="AccontInput" CssClass="input" Enabled="false" autocomplete="off" runat="server"></asp:TextBox>
                
                <span>New Password：</span>
                <asp:TextBox ID="pwdInput" type="text" CssClass="input" runat="server"></asp:TextBox>

                <span>Confirm Password：</span>
                <asp:TextBox ID="confirmPwdInput" type="text" CssClass="input" 
                    runat="server"></asp:TextBox>

            </div>
            <br/>
            <!-- UseSubmitBehavior="false" 添加這個會沒辦法點選enter -->
            <div class="ContainerBtn">
                <asp:Button ID="ReviseButton" CssClass="btn" runat="server" Text="Confirm" OnClick="ReviseButton_Click" /><br/>
                <asp:Label ID="lblMessage" Visible="true" runat="server"></asp:Label>
            </div>
        </div>

         <!-- 確認Box -->
        <div class="confirmbox" id="errorbox" runat="server">
           <div class="checkboxsub">
               <p id="showMessenger" runat="server"></p>
               <asp:Button ID="checkBtn" Text="關閉" CssClass="btn" OnClick="checkBoxBtn_Click" UseSubmitBehavior="false" runat="server"/>
           </div>
        </div>

    </form>
</body>
</html>
