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
                <div class="TBoxRow">
                    <span class="title">Title</span>
                    <asp:TextBox ID="txtR" CssClass="input" runat="server"></asp:TextBox>
                    <span class="title">Update</span>
                    <asp:TextBox ID="txtRD" CssClass="date" Enabled="false" runat="server"></asp:TextBox>
                </div>
                <br><br/>
                <span class="title">Guest Book</span>
                <div class="TBoxRow">
                   <asp:TextBox ID="txtRGB" CssClass="inputGB" TextMode="MultiLine" Rows="20" runat="server"></asp:TextBox>
                </div>

                <div class="BtRow">
                    <asp:Button ID="btnR" CssClass="btn" runat="server" Text="確認修改" Onclick="btnR_Click" />
                    <asp:Button ID="btnB" CssClass="btn" runat="server" Text="返回" Onclick="btnB_Click" />
                </div>
        </div>
    </form>
</body>
</html>
