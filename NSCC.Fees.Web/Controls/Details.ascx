﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="NSCC.Fees.Web.Controls.Details" %>

<%@ Import Namespace="NSCC.Fees.Data" %>
<%@ Import Namespace="NSCC.Fees.Business" %>

<asp:PlaceHolder runat="server" ID="plcFound">


<h1><asp:Label ID="lblProgramFees" Runat="server" Text="{0} Estimated Fees"></asp:Label><br /><span id="lblProgram"><asp:Label ID="lblTitle" Runat="server"></asp:Label></span></h1>



<p>Tuition, fees and costs listed on this page are for <strong>planning purposes only</strong>. They do not represent final amounts owing and are in Canadian dollars (CDN).</p>

<div>
<asp:PlaceHolder runat="server" ID="plcProgramPageLink" Visible="false">For details about this program visit the <a runat="server" id="lnkProgramPage">program page</a>.</asp:PlaceHolder><asp:PlaceHolder runat="server" ID="plcInternationalText" Visible="false"> International students are not eligible for this program.</asp:PlaceHolder>
</div>

<table class="tablesaw tablesaw-stack fees-3c" data-tablesaw-mode="stack">
    <thead>
    <tr>
        <th>Tuition and college fees</th>
        <th>Domestic amount (CDN$)</th>
        <asp:PlaceHolder runat="server" ID="plcColIntAmount" Visible="false"><th>International amount (CDN$)</th></asp:PlaceHolder>
    </tr>
    </thead>
    <tr>
        <td>Tuition</td>
        <td><asp:Literal runat="server" ID="litTuitionDomestic" /></td>
        <asp:PlaceHolder runat="server" ID="plcColIntTuition" Visible="false"><td><asp:Literal runat="server" ID="litTuitionInternational" /></td></asp:PlaceHolder>
    </tr>
    <asp:PlaceHolder runat="server" ID="plcCollegeServiceFee" Visible="false">
    <tr>
        <td>College service fee</td>
        <td><asp:Literal runat="server" ID="litCollegeServiceFeeDomestic" /></td>
        <asp:PlaceHolder runat="server" ID="plcColIntCollegeServiceFee" Visible="false"><td><asp:Literal runat="server" ID="litCollegeServiceFeeInternational" /></td></asp:PlaceHolder>
    </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder runat="server" ID="plcHealthAndDental" Visible="false">
    <tr>
        <td>Student health and dental benefits<span class="health-dental-info tooltip-icon"><span class="fa-stack" aria-hidden="true"><span class="fa fa-circle fa-stack-1x icon-bg-white"></span><span class="fa fa-info-circle fa-stack-1x"></span></span></span><span class="health-dental-info-tooltip tooltip-info">This fee applies to all International and/or full-time students in eligible programs - unless proof of comparable coverage is provided. For more information, visit <a href="/admissions/cost_and_financial_aid/health-and-dental-benefits.asp">student health and dental plan</a>.</span></td>
        <td><asp:Literal runat="server" ID="litHealthAndDentalDomestic" /></td>
       <asp:PlaceHolder runat="server" ID="plcColIntHealthandDental" Visible="false"><td><asp:Literal runat="server" ID="litHealthAndDentalInternational" /></td></asp:PlaceHolder>
    </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder runat="server" ID="plcStudentAssociationFee" Visible="false">
    <tr>
        <td>Student Association fee<span class="sa-fee-info tooltip-icon qtip-linebreak"><span class="fa-stack" aria-hidden="true"><span class="fa fa-circle fa-stack-1x icon-bg-white"></span><span class="fa fa-info-circle fa-stack-1x"></span></span></span><span class="sa-fee-info-tooltip tooltip-info">This fee varies by campus. To check your exact amount, view <a href="/admissions/cost_and_financial_aid/safees/index.asp">campus Student Association fees</a>.</span></td>
        <td><asp:Literal runat="server" ID="litStudentAssociationFeeDomestic" /></td>
        <asp:PlaceHolder runat="server" ID="plcColIntStudentAssociation" Visible="false"><td><asp:Literal runat="server" ID="litStudentAssociationFeeInternational" /></td></asp:PlaceHolder>
    </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder runat="server" ID="plcUPass" Visible="false">
    <tr>
        <td>U-Pass<span class="upass-info tooltip-icon"><span class="fa-stack" aria-hidden="true"><span class="fa fa-circle fa-stack-1x icon-bg-white"></span><span class="fa fa-info-circle fa-stack-1x"></span></span></span><span class="upass-info-tooltip tooltip-info">The U-Pass fee applies to full-time students at Akerley, Institute of Technology and Ivany campuses, and is optional for eCampus students.</span></td>
        <td><asp:Literal runat="server" ID="litUPassDomestic" /></td>
        <asp:PlaceHolder runat="server" ID="plcColIntUPass" Visible="false"><td><asp:Literal runat="server" ID="litUPassInternational" /></td></asp:PlaceHolder>
    </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder runat="server" ID="plcParkingPass" Visible="false">
    <tr>
        <td>Parking pass<span class="parkingpass-info tooltip-icon"><span class="fa-stack" aria-hidden="true"><span class="fa fa-circle fa-stack-1x icon-bg-white"></span><span class="fa fa-info-circle fa-stack-1x"></span></span></span><span class="parkingpass-info-tooltip tooltip-info">This fee applies only if parking on NSCC property. If a parking pass isn't required, deduct this fee from your total.</td>
        <td><asp:Literal runat="server" ID="litParkingPassDomestic" /></td>
        <asp:PlaceHolder runat="server" ID="plcColIntParkingPass" Visible="false"><td><asp:Literal runat="server" ID="litParkingPassInternational" /></td></asp:PlaceHolder>
    </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder runat="server" ID="plcInternationalStudentFee" Visible="false">
    <tr>
        <td>International student fee</td>
        <td>--</td>
        <td><asp:Literal runat="server" ID="litInternationalStudentFeeInternational" /></td>
    </tr>
    </asp:PlaceHolder>
    <tr class="fees-total-row">
        <td>Total</td>
        <td><asp:Literal runat="server" ID="litTotalDomestic" /></td>
        <asp:PlaceHolder runat="server" ID="plcColDomTotal" Visible="false"><td><asp:Literal runat="server" ID="litTotalInternational" /></td></asp:PlaceHolder>
    </tr>
    <tr class="fees-total-row">
        <td>Payment (1st term)<span class="paymentfirstterm-info tooltip-icon qtip-linebreak"><span class="fa-stack" aria-hidden="true"><span class="fa fa-circle fa-stack-1x icon-bg-white"></span><span class="fa fa-info-circle fa-stack-1x"></span></span></span><span class="paymentfirstterm-info-tooltip tooltip-info">If you paid a non-refundable tuition deposit, subtract it from this total.</span></td>
        <td><asp:Literal runat="server" ID="litPaymentFirstTermDomestic" /></td>
        <asp:PlaceHolder runat="server" ID="plcColIntTotal" Visible="false"><td><asp:Literal runat="server" ID="litPaymentFirstTermInternational" /></td></asp:PlaceHolder>
    </tr>
</table>

<asp:PlaceHolder runat="server" ID="plcNotesPayment" Visible="false">
<div class="fees-notes">
<asp:Literal runat="server" ID="litNotesPayment" />
</div>
</asp:PlaceHolder>

<asp:PlaceHolder runat="server" ID="plcNotesTuition" Visible="false">
<div class="fees-notes">
<asp:Literal runat="server" ID="litNotesTuition" />
</div>
</asp:PlaceHolder>


<asp:PlaceHolder runat="server" ID="plcCoop" Visible="false">
<table class="tablesaw tablesaw-stack fees-3c" data-tablesaw-mode="stack">
    <thead>
    <tr>
        <th>Cooperative Education (Co-op)</th>
        <th>Domestic amount</th>
        <asp:PlaceHolder runat="server" ID="plcColCoop" Visible="false"><th>International amount</th></asp:PlaceHolder>
    </tr>
    </thead>
    <tr>
        <td><asp:Literal runat="server" ID="litCoop" /></td>
        <td><asp:Literal runat="server" ID="litCoopDomestic" /></td>
        <asp:PlaceHolder runat="server" ID="plcColIntCoop" Visible="false"><td><asp:Literal runat="server" ID="litCoopInternational" /></td></asp:PlaceHolder>
    </tr>
</table>
</asp:PlaceHolder>


<asp:PlaceHolder runat="server" ID="plcNotesCoop" Visible="false">
<div class="fees-notes">
<asp:Literal runat="server" ID="litNotesCoop" />
</div>
</asp:PlaceHolder>


<asp:PlaceHolder runat="server" ID="plcTextbooks" Visible="false">
<table class="tablesaw tablesaw-stack fees-3c" data-tablesaw-mode="stack">
    <thead>
    <tr>
        <th>Textbooks (estimate)</th>
        <th>Domestic amount</th>
        <asp:PlaceHolder runat="server" ID="plcColTextbooks" Visible="false"><th>International amount</th></asp:PlaceHolder>
    </tr>
    </thead>
    <tr>
        <td>Textbooks</td>
        <td><asp:Literal runat="server" ID="litTextbooksDomestic" /></td>
        <asp:PlaceHolder runat="server" ID="plcColIntTextbooks" Visible="false"><td><asp:Literal runat="server" ID="litTextbooksInternational" /></td></asp:PlaceHolder>
    </tr>
</table>
</asp:PlaceHolder>

<asp:PlaceHolder runat="server" ID="plcNotesTextbooks" Visible="false">
<div class="fees-notes">
<asp:Literal runat="server" ID="litNotesTextbooks" />
</div>
</asp:PlaceHolder>


<asp:PlaceHolder runat="server" ID="plcSupplies" Visible="false">
<table class="tablesaw tablesaw-stack fees-3c fees-no-notes" data-tablesaw-mode="stack">
    <thead>
    <tr>
        <th>Classroom/Portfolio Supplies</th>
        <th>Domestic amount</th>
        <asp:PlaceHolder runat="server" ID="plcColSupplies" Visible="false"><th>International amount</th></asp:PlaceHolder>
    </tr>
    </thead>
    <tr>
        <td>Classroom/Portfolio Supplies</td>
        <td><asp:Literal runat="server" ID="litSuppliesDomestic" /></td>
        <asp:PlaceHolder runat="server" ID="plcColIntSupplies" Visible="false"><td><asp:Literal runat="server" ID="litSuppliesInternational" /></td></asp:PlaceHolder>
    </tr>
</table>
</asp:PlaceHolder>
<asp:PlaceHolder runat="server" ID="plcCostItems">
<asp:Repeater runat="server" ID="rptCostItems" OnItemDataBound="rptCostItems_ItemDataBound">
    <HeaderTemplate><table class="tablesaw tablesaw-stack fees-3c" data-tablesaw-mode="stack">
        <thead>
        <tr>
            <th>Additional program costs</th>
            <th>Domestic amount</th>
            <asp:PlaceHolder runat="server" ID="plcColCostItems"><th>International amount</th></asp:PlaceHolder>
        </tr>
        </thead>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td><%# ((CostItem)Container.DataItem).Name %></td>
            <td><%# String.Format(Constants.COMMA_FORMAT, ((CostItem)Container.DataItem).Cost) %></td>
            <asp:PlaceHolder runat="server" ID="plcIntCostItems"><td><%# String.Format(Constants.COMMA_FORMAT, ((CostItem)Container.DataItem).Cost) %></td></asp:PlaceHolder>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        <tr class="fees-total-row">
            <td>Total (HST not included)</td>
            <td><asp:Literal runat="server" ID="litCostItemsTotalDomestic" /></td>
            <asp:PlaceHolder runat="server" ID="plcIntColCostItemsTotal"><td><asp:Literal runat="server" ID="litCostItemsTotalInternational" /></td></asp:PlaceHolder>
        </tr>
        </table>
    </FooterTemplate>
</asp:Repeater>
<div class="fees-indented">These are estimated costs for items required for your program. If you already have any of these items, they may meet your program requirements.</div>
</asp:PlaceHolder>

<asp:PlaceHolder runat="server" ID="plcNotesCostItems" Visible="false">
<div class="fees-notes">
<asp:Literal runat="server" ID="litNotesCostItems" />
</div>
</asp:PlaceHolder>



<table class="tablesaw tablesaw-stack fees-3c">
    <thead>
    <tr>
        <th>Total Cost</th>
        <th>Domestic total</th>
        <asp:PlaceHolder runat="server" ID="plcColTotalCost" Visible="false"><th>International total</th></asp:PlaceHolder>
    </tr>
    </thead>
    <tr class="fees-total-row">
        <td>Tuition and college fees + co-op (if applicable) + textbooks  + classroom/portfolio supplies + additional program costs</td>
        <td><asp:Literal runat="server" ID="litTotalCostDomestic" /></td>
        <asp:PlaceHolder runat="server" ID="plcColIntTotalCost" Visible="false"><td><asp:Literal runat="server" ID="litTotalCostInternational" /></td></asp:PlaceHolder>
    </tr>
</table>

<asp:PlaceHolder runat="server" ID="plcSchedules" Visible="false">
<h2>Schedule and payments</h2>
<p>
    Visit our <a runat="server" id="lnkAcademicCalendar">Academic Calendar</a> to see the full list of important dates.
</p>
<asp:Repeater runat="server" ID="rptSchedules">
    <HeaderTemplate><table class="tablesaw tablesaw-stack fees-schedule" data-tablesaw-mode="stack">
        <thead>
        <tr>
            <th>Campus</th>
            <th>Start date</th>
            <th>Academic year end date</th>
            <th>Program end date</th>
            <th>Payment due (1st term)</th>
            <th>U-Pass fee</th>
        </tr>
        </thead>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td><%# ((Schedule)Container.DataItem).Location.ShortName %></td>
            <td><%# ((Schedule)Container.DataItem).StartDate.Value.ToString("MMM d, yyyy") %></td>
            <td><%# ((Schedule)Container.DataItem).AcademicYearEndDate.Value.ToString("MMM d, yyyy") %></td>
            <td><%# ((Schedule)Container.DataItem).ProgramEndDate.Value.ToString("MMM d, yyyy") %></td>
            <td><%# ((Schedule)Container.DataItem).FirstPaymentDate.HasValue ? ((Schedule)Container.DataItem).FirstPaymentDate.Value.ToString("MMM d, yyyy") : "" %></td>
            <td><%# ((Schedule)Container.DataItem).HasUPass ? "Yes" : "No" %></td>
        </tr>
    </ItemTemplate>
    <FooterTemplate></table></FooterTemplate>
</asp:Repeater>
</asp:PlaceHolder>

<asp:PlaceHolder runat="server" ID="plcNotesSchedule" Visible="false">
<div class="fees-notes">
<div><asp:Literal runat="server" ID="litNotesSchedule" /></div>
</div>
</asp:PlaceHolder>

</asp:PlaceHolder>

<asp:Placeholder runat="server" ID="plcNotFound">
<h1 id="404Hdr">Page Not Found (404 Error)</h1>
<p>We're sorry, the page you're looking for cannot be found.</p>
<p>The link may be incorrect or outdated, or the page might have moved.</p>
<p><a href="/default.aspx">nscc.ca homepage</a> | <a href="/contact_us/default.aspx">Contact us to report a problem</a></p>

<!-- Google CSE Search Box Begins  -->
  <script>
   window.onload = function()
   { 
   // header search box
   var searchBoxHdr =  document.getElementById("gsc-i-id1");
   searchBoxHdr.placeholder="Search nscc.ca";
   searchBoxHdr.title="Search nscc.ca";
   // search page search box
   var searchBox =  document.getElementById("gsc-i-id2");
   searchBox.placeholder="Search nscc.ca";
   searchBox.title="Search nscc.ca";
   }
  </script>
<div id="search-box">
    <div class="gcse-searchbox-only" data-resultsUrl="/search/index.asp"></div>
</div>
<!-- Google CSE Search Box Ends -->
</asp:Placeholder>