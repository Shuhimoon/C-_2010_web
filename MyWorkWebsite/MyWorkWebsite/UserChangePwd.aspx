<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserChangePwd.aspx.cs" Inherits="MyWorkWebsite.UserChangePwd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User</title>
    <link href="Styles/SubPage.css" rel="Stylesheet" />
    <!--[if IE]><link href="IE_SubPage/IESubPage.css" type="text/css" rel="Stylesheet"/><![endif]-->
</head>
<body>
    <form id="form1" runat="server">
    <div class="namefield">
        <span class="title">Name :</span>
        <asp:TextBox id="nameSelect" CssClass="input" runat="server" MaxLength="25" ></asp:TextBox>
        <asp:Button id="selectBtn" Text="select" CssClass="btn" OnClick="selectBtn_Click" UseSubmitBehavior="true" runat="server" />
        <asp:Label ID="lblMessage" Visible="true" runat="server"></asp:Label>
    </div>

    <!-- 確認Box -->
    <div class="confirmbox" id="errorbox" runat="server">
       <div class="checkboxsub">
           <p id="showMessenger" runat="server"></p>
           <asp:Button ID="checkBtn" Text="關閉" CssClass="btn" OnClick="checkBoxBtn_Click" UseSubmitBehavior="false" runat="server"/>
       </div>
    </div>

    <!-- 顯示內容 -->
    <div class="userlist">
            <asp:Repeater ID="rptUserList" runat="server" OnItemCommand="rptUserList_ItemCommand">
                <ItemTemplate>
                    <div class="entry-card">
                        <div class="entry-header">
                            <span class="entry-title"><%# Eval("UserName") %></span>
                        </div>
                        <div class="entry-actions">
                            <asp:Button ID="btnRemake" runat="server" Text="Remake" CommandName="RemakeEntry" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-warning"
                                OnClientClick="return confirm('Are you sure you want to Remake this entry !!!!');"  />
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="DeleteEntry" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-danger"
                                OnClientClick="return confirm('Are you sure you want to delete this entry?');" /><!-- 確認是否刪除 -->
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
