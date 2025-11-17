<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="/GBD.aspx.cs" Inherits="GuestBook.GBD" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Delete</title>
    <link href="../styles/gb.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="Container">
            <h2>Delete</h2>   
                <div class="TBoxRow">
                    <span class="title">Title</span>
                    <asp:TextBox ID="txtD" CssClass="input" Enabled="false" runat="server"></asp:TextBox>
                    <span class="title">Update</span>
                    <asp:TextBox ID="txtDD" CssClass="date" Enabled="false" runat="server"></asp:TextBox>
                </div>
                <br><br/>
                <span class="title">Guset Book</span>
                <div class="TBoxRow">
                   <asp:TextBox ID="txtDGB" CssClass="inputGB" TextMode="MultiLine" Rows="20" Enabled="false" runat="server"></asp:TextBox>
                </div>

                <div class="BtRow">
                    <asp:Button ID="btnD" CssClass="btn" runat="server" Text="確認刪除" Onclick="btnD_Click" />
                    <asp:Button ID="btnB" CssClass="btn" runat="server" Text="返回" Onclick="btnB_Click" />
                </div>
        </div>
    </form>
</body>
</html>