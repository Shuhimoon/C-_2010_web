<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GBN.aspx.cs" Inherits="GuestBook.Styles.GBN" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>New</title>
    <link href="../styles/gb.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="Container">
            <h2>New Guest Book</h2>   
                <div class="TBoxRow">
                    <span class="title">Title</span>
                    <asp:TextBox ID="txtN" CssClass="input" runat="server"></asp:TextBox>
                    <span class="title">Date</span>
                    <asp:TextBox ID="txtND" CssClass="date" Enabled="false" runat="server"></asp:TextBox>
                </div>
                <br><br/>
                <span class="title">Guest Book</span>
                <div class="TBoxRow">
                   <asp:TextBox ID="txtNGB" CssClass="inputGB" TextMode="MultiLine" Rows="20" runat="server"></asp:TextBox>
                </div>

                <div class="BtRow">
                    <asp:Button ID="btnC" CssClass="btn" runat="server" Text="確認新增" Onclick="btnC_Click" />
                    <asp:Button ID="btnB" CssClass="btn" runat="server" Text="返回" Onclick="btnB_Click" />
                </div>
        </div>
    </form>
</body>
</html>
