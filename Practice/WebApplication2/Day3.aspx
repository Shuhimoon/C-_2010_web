<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Day3.aspx.cs" Inherits="WebApplication2.Day3" %>


<asp:Content ID="Content_HeadDay3" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="styles/site.css" rel="stylesheet" />
</asp:Content>

 <asp:Content ID="Content_Day3" ContentPlaceHolderID="MainContent" runat="server">  
        <h2>Day3 : Page Life Cycle / ViewState 練習 </h2>

        <asp:Button ID="btnCount" runat="server" Text="Click Me !!! +1 +1" OnClick="btnCount_Click"/>
        <br/><br/>

        <asp:Label ID="lblCount" runat="server" Text=""></asp:Label>
        <br/><br/>

        <asp:Label ID="lblLog" runat="server" Text=""></asp:Label>
        <asp:HiddenField ID="hidCount" runat="server" />
</asp:Content>
 
