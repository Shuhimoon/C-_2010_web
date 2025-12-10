<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="MyWorkWebsite.List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>List</title>
    <link rel="icon" type="image/x-icon" href="Styles/cat.png" />
    <link href="Styles/Default.css" rel="Stylesheet" />
    <!--[if IE]><link href="IE_Styles/IEDefault.css" type="text/css" rel="Stylesheet"/><![endif]-->
</head>
<body>
    <form id="form1" runat="server">
        <h1 class="bk">Shuhi Website</h1>
        <h1>Shuhi Website</h1>

        <div class="listContainer">
            <h3>Directory</h3>
            <hr/>
            <asp:LinkButton ID="HomePage" CssClass="linkBtn" OnClick="lnkHPage_Click" runat="server">Home</asp:LinkButton><br/><br/>
            <asp:LinkButton ID="UserPage" CssClass="linkBtn" OnClick="lnkUserPage_Click" runat="server">User</asp:LinkButton><br/><br/>
            <asp:LinkButton ID="RedispatchPage" CssClass="linkBtn" OnClick="lnkRedispatchPage_Click" runat="server">改簽派人員</asp:LinkButton><br/><br/>
            <asp:LinkButton ID="UploadPage" CssClass="linkBtn" OnClick="lnkUploadPage_Click" runat="server">Upload File</asp:LinkButton><br/><br/>
            <asp:LinkButton ID="logout" CssClass="lgBtn" OnClick="logoutPage_Click" runat="server"></asp:LinkButton> 
        </div>
        
        <div class="subConainer">
            <iframe id="subPage" name="subPage" src="Home.aspx" frameborder="0" allowtransparency="true" runat="server"></iframe>
        </div>
    </form>
</body>
</html>
