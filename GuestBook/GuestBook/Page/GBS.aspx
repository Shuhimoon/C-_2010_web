<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GBS.aspx.cs" Inherits="GuestBook.GBS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Query</title>
    <link href="../styles/gb.css" rel="stylesheet"/>
</head>
<body>
    <form id="GBSPage" runat="server">
    <asp:SqlDataSource ID="SqlDataCompany" runat="server" ConnectionString="<%$ ConnectionStrings:MyDB %>"
                SelectCommand="SELECT * FROM GuestBook WHERE Title LIKE '%' + @Title + '%' AND CreateDate >= @start AND CreateDate < DATEADD(day,1,@end)">
    </asp:SqlDataSource>
        <div class="Container">
            <h2>Query System</h2>     
            <div class="TBoxRow">
                <span class="title">Title</span>
                <asp:TextBox ID="txtTitle" CssClass="input" runat="server" MaxLength="100"></asp:TextBox>
            </div>
            <br/>
            <div class="TBoxRow">
                <span class="title">Start Date</span>
                <asp:TextBox ID="txtSDate" CssClass="input" runat="server" Enabled="false" MaxLength="10" ></asp:TextBox>
                <!-- 開始日期的日曆 -->
                 <asp:ImageButton  ID="calSBt"  runat="server" ImageUrl="~/cals.png" AlternateText="calSBt"  CssClass="cals"  OnClick="CalSbtn_Click" />

                <span class="title">End Date</span>
                <asp:TextBox ID="txtEDate" CssClass="input" runat="server" Enabled="false" MaxLength="10"></asp:TextBox>
                <!-- 結束日期的日曆 -->
                <asp:ImageButton  ID="calEBt"  runat="server" ImageUrl="~/cals.png" AlternateText="calEBt"  CssClass="cals"  OnClick="CalEbtn_Click" />
            
            </div>
            <div class="calsRow">
                 <asp:Calendar ID="calS" runat="server" OnSelectionChanged="calS_SelectionChanged" Visible="false" CssClass="cals-popup"></asp:Calendar>
                <asp:Calendar ID="calE" runat="server" OnSelectionChanged="calE_SelectionChanged" Visible="false" CssClass="cale-popup"></asp:Calendar>
            </div> 
            <div class="BtRow">
                <asp:Button ID="btnS" CssClass="btn" runat="server" Text="搜尋" OnClick="btnS_Click" />
                <asp:Button ID="btnN" CssClass="btn" runat="server" Text="新增" Onclick="btnN_Click" />
                <asp:Button ID="btnU" CssClass="btn" runat="server" Text="修改" Onclick="btnU_Click" />
            </div>

           
            
            <!--顯示內容-->
            <br><br/>
            <asp:Literal ID="ltResult" runat="server"></asp:Literal>

        </div>
           
    </form>
</body>
</html>
