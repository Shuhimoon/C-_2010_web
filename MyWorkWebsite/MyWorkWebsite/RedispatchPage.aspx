<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RedispatchPage.aspx.cs" Inherits="MyWorkWebsite.RedispatchPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="icon" type="image/x-icon" href="Styles/cat.png" />
    <link href="Styles/SubPage.css" rel="Stylesheet" />
    <!--[if IE]><link href="IE_SubPage/IESubPage.css" type="text/css" rel="Stylesheet"/><![endif]-->
</head>
<body>
    <form id="form1" runat="server">
        <div class="namefield">
                <span>Reviser people：</span>
                <asp:TextBox ID="RPInput" type="text" CssClass="input" runat="server"></asp:TextBox>

                <span>AppNo：</span>
                <asp:TextBox ID="AppOnInput" CssClass="input" autocomplete="off" runat="server"></asp:TextBox>

                <span>Page Code：</span>
                <asp:DropDownList ID="PgDD" runat="server" CssClass="dropdown">
                    <asp:ListItem Text="缺點改正回覆審查" Value="1"></asp:ListItem>
                    <asp:ListItem Text="適航證書審查" Value="2"></asp:ListItem>
                    <asp:ListItem Text="航空器大修理" Value="3"></asp:ListItem>
                </asp:DropDownList>
        </div>
        <span class="Ex">Explain
            <div>
            <table border=1 class='tb'>
                <tr>
                    <td>代辦狀態</td>
                    <td>簽審狀態</td>
                    <td>主檔狀態</td>
                    <td>備註</td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td>NON 未送審</td>
                    <td></td>
                </tr>
                <tr>
                    <td>AS 分派</td>
                    <td></td>
                    <td>NEX 待分派</td>
                    <td></td>
                </tr>
                <tr>
                    <td>KM 承辦</td>
                    <td>SIN</td>
                    <td>EXG 審查中</td>
                    <td></td>
                </tr>
                <tr style="background-color:#F2F8EC">
                    <td>NR  審查</td>
                    <td>APP 審查</td>
                    <td>EFN 審查完畢</td>
                    <td></td>
                </tr>
                <tr style="background-color:#F2F8EC">
                    <td>NR  決行 </td>
                    <td>CLS 決行</td>
                    <td></td>
                    <td>跳去 CL 結案</td>
                </tr>
                <tr style="background-color:#CEEBC7">
                    <td>CS 會知 (通知)</td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr style="background-color:#ECF6F8">
                    <td>RK 駁回</td>
                    <td></td>
                    <td>RJT 退件</td>
                    <td></td>
                </tr>
                <tr style="background-color:#ECF6F8">
                    <td>RK 駁回</td>
                    <td></td>
                    <td>DOC 待補件</td>
                    <td></td>
                </tr>
                <tr style="background-color:#FFE3E3">
                    <td>CL 結案</td>
                    <td></td>
                    <td>TEP 暫時結案</td>
                    <td></td>
                </tr>
                <tr style="background-color:#FFE3E3">
                    <td>CL 結案</td>
                    <td></td>
                    <td>FIN 結案</td>
                    <td></td>
                </tr>
            </table>
        </div>
        
        </span>

        

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
