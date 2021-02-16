<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="NSCC.Fees.Web.Default" %>
<%@ Register TagPrefix="uc" TagName="AtoZ" Src="~/Controls/AtoZ.ascx" %>
<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc:AtoZ runat="server" ID="AtoZ"></uc:AtoZ>
    </div>
    </form>
</body>
</html>
