<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AirplaneStats.aspx.cs" Inherits="airplan.AirplaneStats" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>User Monthly Statistics</title>
    <style type="text/css">
        /* CSS 2.1 compatible with IE7 and modern browsers */
        body {
            font-family: Arial, sans-serif;
            margin: 10px;
        }
        table {
            border-collapse: collapse;
            width: 80%;
        }
        th, td {
            border: 1px solid #000000;
            padding: 5px;
            text-align: left;
        }
        th {
            background-color: #CCCCCC;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>User ID with Monthly Record Counts (e.g., 2025/11, 2025/12, etc.)</h2>
        <asp:GridView ID="GridView1" runat="server" 
                      AutoGenerateColumns="true" 
                      CssClass="gridTable">
        </asp:GridView>
    </div>
    </form>
</body>
</html>