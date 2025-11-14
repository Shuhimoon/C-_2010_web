<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Day4.aspx.cs" Inherits="WebApplication2.Day4" %>

<asp:Content ID="Content_HeadDay4" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="styles/site.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content_Day4" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Day4 : GridView + DataTable </h2>
    <asp:Button ID="btnload" runat="server" Text="inputdata" OnClick="btnLoad_Click"/>
    <br/><br/>

    <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" OnRowCommand="gvData_RowCommand">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="Price" HeaderText="Price" />
            <%-- 注意這邊是Button 不是Bound --%>
            <asp:ButtonField HeaderText="Show" CommandName="Show"  Text="Show" />
        </Columns>
    </asp:GridView>
    <br/><br/>

    <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
     
</asp:Content>