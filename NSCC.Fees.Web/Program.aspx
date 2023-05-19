<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Program.aspx.cs" Inherits="NSCC.Fees.Web.Program" %>
<%@ Register TagPrefix="uc" TagName="Details" Src="~/Controls/Details.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
	<link href="https://www.nscc.ca/inc/css/styles.css?v=3.23" rel="stylesheet"/>
    <link href="https://www.nscc.ca/inc/css/tablesaw.stackonly.css?v=1.0" rel="stylesheet"/>
    <link href="https://www.nscc.ca/inc/css/programs.css?v=2.9" rel="stylesheet" />

    <script src="https://www.nscc.ca/inc/js/jquery/jquery-3.5.1.min.js"></script>
    <script src="https://www.nscc.ca/inc/js/jquery/popper.min.js"></script>
    <script src="https://www.nscc.ca/inc/js/jquery/tippy-bundle.umd.min.js"></script>

    <script src="https://www.nscc.ca/inc/js/nscc/fees-page.js?v=1.0"></script>
    <script src="https://www.nscc.ca/inc/js/nscc/program-page.js?v=1.6"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc:Details runat="server"></uc:Details>
    </div>
    </form>
</body>
</html>
