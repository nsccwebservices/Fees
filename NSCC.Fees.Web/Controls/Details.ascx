<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="NSCC.Fees.Web.Controls.Details" %>

<%@ Import Namespace="NSCC.Fees.Data" %>

<h1><span id="lblProgram"><asp:Label ID="lblTitle" Runat="server" Text="Fees - {0}"></asp:Label></span></h1>

<h2><asp:Label ID="lblProgramFees" Runat="server" Text="{0} Program Fees"></asp:Label></h2>

<p>Global Intro copy - show on every program fees page.</p>

<asp:PlaceHolder runat="server" ID="plcInternationalText" Visible="false">Not available to international students.</asp:PlaceHolder>

<table class="tablesaw tablesaw-stack" data-tablesaw-mode="stack">
    <thead>
    <tr>
        <td>Tuition and college fees</td>
        <td>Domestic amount</td>
        <asp:PlaceHolder runat="server" ID="plcColIntAmount" Visible="false"><td>International amount</td></asp:PlaceHolder>
    </tr>
    </thead>
    <tr>
        <td>&nbsp;</td>
        <td>(Can$)</td>
        <asp:PlaceHolder runat="server" ID="plcColCanadianDollars" Visible="false"><td>(Can$)</td></asp:PlaceHolder>
    </tr>
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
        <td>Student health &amp; dental benefits</td>
        <td><asp:Literal runat="server" ID="litHealthAndDentalDomestic" /></td>
       <asp:PlaceHolder runat="server" ID="plcColIntHealthandDental" Visible="false"><td><asp:Literal runat="server" ID="litHealthAndDentalInternational" /></td></asp:PlaceHolder>
    </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder runat="server" ID="plcStudentAssociationFee" Visible="false">
    <tr>
        <td>Student Association fee</td>
        <td><asp:Literal runat="server" ID="litStudentAssociationFeeDomestic" /></td>
        <asp:PlaceHolder runat="server" ID="plcColIntStudentAssociation" Visible="false"><td><asp:Literal runat="server" ID="litStudentAssociationFeeInternational" /></td></asp:PlaceHolder>
    </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder runat="server" ID="plcUPass" Visible="false">
    <tr>
        <td>U-Pass</td>
        <td><asp:Literal runat="server" ID="litUPassDomestic" /></td>
        <asp:PlaceHolder runat="server" ID="plcColIntUPass" Visible="false"><td><asp:Literal runat="server" ID="litUPassInternational" /></td></asp:PlaceHolder>
    </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder runat="server" ID="plcParkingPass" Visible="false">
    <tr>
        <td>Parking pass</td>
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
    <tr>
        <td>Total</td>
        <td><asp:Literal runat="server" ID="litTotalDomestic" /></td>
        <asp:PlaceHolder runat="server" ID="plcColDomTotal" Visible="false"><td><asp:Literal runat="server" ID="litTotalInternational" /></td></asp:PlaceHolder>
    </tr>
    <tr>
        <td>Payment (1st term)</td>
        <td><asp:Literal runat="server" ID="litPaymentFirstTermDomestic" /></td>
        <asp:PlaceHolder runat="server" ID="plcColIntTotal" Visible="false"><td><asp:Literal runat="server" ID="litPaymentFirstTermInternational" /></td></asp:PlaceHolder>
    </tr>
</table>

<asp:PlaceHolder runat="server" ID="plcNotesTuition" Visible="false">
<asp:Literal runat="server" ID="litNotesTuition" />
</asp:PlaceHolder>

<asp:PlaceHolder runat="server" ID="plcCoop" Visible="false">
<table class="tablesaw tablesaw-stack" data-tablesaw-mode="stack">
    <thead>
    <tr>
        <td>Cooperative Education (Co-op)</td>
        <td>Domestic amount</td>
        <asp:PlaceHolder runat="server" ID="plcColCoop" Visible="false"><td>International amount</td></asp:PlaceHolder>
    </tr>
    </thead>
    <tr>
        <td><asp:Literal runat="server" ID="litCoop" Text="Co-op course tuition ({0}) - this is an academic course and students are required to pay course tuition for one unit."/></td>
        <td><asp:Literal runat="server" ID="litCoopDomestic" /></td>
        <asp:PlaceHolder runat="server" ID="plcColIntCoop" Visible="false"><td><asp:Literal runat="server" ID="litCoopInternational" /></td></asp:PlaceHolder>
    </tr>
</table>
</asp:PlaceHolder>

<asp:PlaceHolder runat="server" ID="plcNotesCoop" Visible="false">
<asp:Literal runat="server" ID="litNotesCoop" />
</asp:PlaceHolder>


<table class="tablesaw tablesaw-stack" data-tablesaw-mode="stack">
    <thead>
    <tr>
        <td>Textbooks (estimate)</td>
        <td>Domestic amount</td>
        <asp:PlaceHolder runat="server" ID="plcColTextbooks" Visible="false"><td>International amount</td></asp:PlaceHolder>
    </tr>
    </thead>
    <tr>
        <td>Textbooks</td>
        <td><asp:Literal runat="server" ID="litTextbooksDomestic" /></td>
        <asp:PlaceHolder runat="server" ID="plcColIntTextbooks" Visible="false"><td><asp:Literal runat="server" ID="litTextbooksInternational" /></td></asp:PlaceHolder>
    </tr>
</table>
<asp:PlaceHolder runat="server" ID="plcNotesTextbooks" Visible="false">
<asp:Literal runat="server" ID="litNotesTextbooks" />
</asp:PlaceHolder>


<table class="tablesaw tablesaw-stack" data-tablesaw-mode="stack">
    <thead>
    <tr>
        <td>Classroom/Portfolio Supplies</td>
        <td>Domestic amount</td>
        <asp:PlaceHolder runat="server" ID="plcColSupplies" Visible="false"><td>International amount</td></asp:PlaceHolder>
    </tr>
    </thead>
    <tr>
        <td>Classroom/Portfolio Supplies</td>
        <td><asp:Literal runat="server" ID="litSuppliesDomestic" /></td>
        <asp:PlaceHolder runat="server" ID="plcColIntSupplies" Visible="false"><td><asp:Literal runat="server" ID="litSuppliesInternational" /></td></asp:PlaceHolder>
    </tr>
</table>


<asp:PlaceHolder runat="server" ID="plcCostItems">
<asp:Repeater runat="server" ID="rptCostItems" OnItemDataBound="rptCostItems_ItemDataBound">
    <HeaderTemplate><table class="tablesaw tablesaw-stack" data-tablesaw-mode="stack">
        <thead>
        <tr>
            <td>Additional program costs<span class="blended-delivery-info"><span class="fa-stack"><span class="fa fa-circle fa-stack-1x icon-bg-white"></span><span class="fa fa-info-circle fa-stack-1x"></span></span></span><span class="blended-delivery-info-tooltip"><strong>Additional program costs</strong><br />If you already have these items...</span></td>
            <td>Domestic amount</td>
            <asp:PlaceHolder runat="server" ID="plcColCostItems"><td>International amount</td></asp:PlaceHolder>
        </tr>
        </thead>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td><%# ((CostItem)Container.DataItem).Name %></td>
            <td><%# ((CostItem)Container.DataItem).Cost.ToString() %></td>
            <asp:PlaceHolder runat="server" ID="plcIntCostItems"><td><%# ((CostItem)Container.DataItem).Cost.ToString() %></td></asp:PlaceHolder>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        <tr>
            <td>Total (HST not incuded)</td>
            <td><asp:Literal runat="server" ID="litCostItemsTotalDomestic" /></td>
            <asp:PlaceHolder runat="server" ID="plcIntColCostItemsTotal"><td><asp:Literal runat="server" ID="litCostItemsTotalInternational" /></td></asp:PlaceHolder>
        </tr>
        </table>
    </FooterTemplate>
</asp:Repeater>
</asp:PlaceHolder>


<asp:PlaceHolder runat="server" ID="plcNotesCostItems" Visible="false">
<asp:Literal runat="server" ID="litNotesCostItems" />
</asp:PlaceHolder>


<table class="tablesaw tablesaw-stack" data-tablesaw-mode="stack">
    <thead>
    <tr>
        <td>Total Cost = Tuition and college fees + co-op (if applicable) + textbooks  + classroom/portfolio supplies + additional program costs</td>
        <td>Domestic total</td>
        <asp:PlaceHolder runat="server" ID="plcColTotalCost" Visible="false"><td>International total</td></asp:PlaceHolder>
    </tr>
    </thead>
    <tr>
        <td>&nbsp;</td>
        <td><asp:Literal runat="server" ID="litTotalCostDomestic" /></td>
        <asp:PlaceHolder runat="server" ID="plcColIntTotalCost" Visible="false"><td><asp:Literal runat="server" ID="litTotalCostInternational" /></td></asp:PlaceHolder>
    </tr>
</table>
<asp:PlaceHolder runat="server" ID="plcNotesPayment" Visible="false">
<asp:Literal runat="server" ID="litNotesPayment" />
</asp:PlaceHolder>



<asp:PlaceHolder runat="server" ID="plcSchedules" Visible="false">
<h2>Schedule and payments</h2>
<asp:Repeater runat="server" ID="rptSchedules">
    <HeaderTemplate><table class="tablesaw tablesaw-stack" data-tablesaw-mode="stack">
        <thead>
        <tr>
            <td>Campus</td>
            <td>Start date</td>
            <td>Academic year end date</td>
            <td>Program end date</td>
            <td>Payment due (1st term)</td>
            <td>U-Pass fee</td>
        </tr>
        </thead>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td><%# ((Schedule)Container.DataItem).Location.LongName %></td>
            <td><%# ((Schedule)Container.DataItem).StartDate.Value.ToString("MMM d, yyyy") %></td>
            <td><%# ((Schedule)Container.DataItem).AcademicYearEndDate.Value.ToString("MMM d, yyyy") %></td>
            <td><%# ((Schedule)Container.DataItem).ProgramEndDate.Value.ToString("MMM d, yyyy") %></td>
            <td><%# ((Schedule)Container.DataItem).FirstPaymentDate.Value.ToString("MMM d, yyyy") %></td>
            <td><%# ((Schedule)Container.DataItem).HasUPass ? "Yes" : "No" %></td>
        </tr>
    </ItemTemplate>
    <FooterTemplate></table></FooterTemplate>
</asp:Repeater>
<div>
    <a runat="server" id="lnkAcademicCalendar">View the NSCC Academic Calendar</a>
</div>
</asp:PlaceHolder>

<asp:PlaceHolder runat="server" ID="plcNotesSchedule" Visible="false">
<div><asp:Literal runat="server" ID="litNotesSchedule" /></div>
</asp:PlaceHolder>

<asp:PlaceHolder runat="server" ID="plcProgramPageLink" Visible="false">
<div><a runat="server" id="lnkProgramPage">View program page</a></div>
</asp:PlaceHolder>
