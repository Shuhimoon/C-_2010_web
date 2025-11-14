<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Day2.aspx.cs" Inherits="WebApplication2.Day2" %>

<asp:Content ID="Content_HeadDay4" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="styles/site.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content_Day4" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Day 2 Practice </h2>
    <!-- DropDownList -->
    <asp:DropDownList ID="ddlFruits" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFruits_SelectedIndexChanged">
    </asp:DropDownList>

    <br/><br/>

    <!-- RadioButtonList -->
    <asp:RadioButtonList ID="rblColor" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblColor_SelectedIndexChanged">
    </asp:RadioButtonList>

    <br/><br/>

    <!-- ListBox -->
    <asp:ListBox  ID="lstAnimals" runat="server" SelectionMode="Multiple" AutoPostBack="true" OnSelectedIndexChanged="lstAnimals_SelectedIndexChanged">
    </asp:ListBox>


    <br/><br/>

    <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
</asp:Content>
