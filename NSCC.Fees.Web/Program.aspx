<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Program.aspx.cs" Inherits="NSCC.Fees.Web.Program" %>
<%@ Register TagPrefix="uc" TagName="Details" Src="~/Controls/Details.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
	<link href="https://www.nscc.ca/inc/css/styles.css?v=2.4" rel="stylesheet"/>
    <link href="https://www.nscc.ca/inc/css/tablesaw.stackonly.css?v=1.0" rel="stylesheet"/>
    <link href="https://www.nscc.ca/inc/css/jquery.qtip2.1.1.min.css" rel="stylesheet" />
    <link href="https://www.nscc.ca/inc/css/programs.css" rel="stylesheet" />

    <script src="https://www.nscc.ca/inc/js/jquery/jquery-1.8.2.min.js"></script>

    <script src="https://www.nscc.ca/inc/js/jquery/jquery.qtip2.1.1.min.js"></script>
    <script src="https://www.nscc.ca/inc/js/nscc/fees-page.js?v=1.0"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc:Details runat="server"></uc:Details>
    </div>
    </form>
</body>
</html>
