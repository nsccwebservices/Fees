<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Details.ascx.cs" Inherits="NSCC.Fees.Web.Controls.Details" %>

<%@ Import Namespace="NSCC.Fees.Data" %>

<h1><span id="lblProgram"><asp:Label ID="lblTitle" Runat="server" Text="Fees - {0}"></asp:Label></span></h1>

<h2><asp:Label ID="lblProgramFees" Runat="server" Text="{0} Program Fees"></asp:Label></h2>

<p>Global Intro copy - show on every program fees page.</p>

<div>
<asp:PlaceHolder runat="server" ID="plcProgramPageLink" Visible="false">For details about this program visit the <a runat="server" id="lnkProgramPage">program page</a>.</asp:PlaceHolder><asp:PlaceHolder runat="server" ID="plcInternationalText" Visible="false"> Not available to international students.</asp:PlaceHolder>
</div>

<table class="tablesaw tablesaw-stack fees-3c" data-tablesaw-mode="stack">
    <thead>
    <tr>
        <th>Tuition and college fees</th>
        <th>Domestic amount (Can$)</th>
        <asp:PlaceHolder runat="server" ID="plcColIntAmount" Visible="false"><th>International amount (Can$)</th></asp:PlaceHolder>
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
        <td>Student health &amp; dental benefits<span class="health-dental-info tooltip-icon"><span class="fa-stack" aria-hidden="true"><span class="fa fa-circle fa-stack-1x icon-bg-white"></span><span class="fa fa-info-circle fa-stack-1x"></span></span></span><span class="health-dental-info-tooltip tooltip-info"><strong>Health &amp; Dental Notes</strong>If you already have these items...</span></td>
        <td><asp:Literal runat="server" ID="litHealthAndDentalDomestic" /></td>
       <asp:PlaceHolder runat="server" ID="plcColIntHealthandDental" Visible="false"><td><asp:Literal runat="server" ID="litHealthAndDentalInternational" /></td></asp:PlaceHolder>
    </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder runat="server" ID="plcStudentAssociationFee" Visible="false">
    <tr>
        <td>Student Association fee<span class="sa-fee-info tooltip-icon"><span class="fa-stack" aria-hidden="true"><span class="fa fa-circle fa-stack-1x icon-bg-white"></span><span class="fa fa-info-circle fa-stack-1x"></span></span></span><span class="sa-fee-info-tooltip tooltip-info">The Student Association fee should be adjusted based on your campus. Student Association (SA) Fee varies by campus - Check <a href="/admissions/cost_and_financial_aid/safees/index.asp">your campus fee</a> to confirm your exact amount.</span></td>
        <td><asp:Literal runat="server" ID="litStudentAssociationFeeDomestic" /></td>
        <asp:PlaceHolder runat="server" ID="plcColIntStudentAssociation" Visible="false"><td><asp:Literal runat="server" ID="litStudentAssociationFeeInternational" /></td></asp:PlaceHolder>
    </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder runat="server" ID="plcUPass" Visible="false">
    <tr>
        <td>U-Pass<span class="upass-info tooltip-icon"><span class="fa-stack" aria-hidden="true"><span class="fa fa-circle fa-stack-1x icon-bg-white"></span><span class="fa fa-info-circle fa-stack-1x"></span></span></span><span class="upass-info-tooltip tooltip-info"><strong>U-Pass Notes</strong>If you already have these items...</span></td>
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
    <tr class="fees-total-row">
        <td>Total</td>
        <td><asp:Literal runat="server" ID="litTotalDomestic" /></td>
        <asp:PlaceHolder runat="server" ID="plcColDomTotal" Visible="false"><td><asp:Literal runat="server" ID="litTotalInternational" /></td></asp:PlaceHolder>
    </tr>
    <tr class="fees-total-row">
        <td>Payment (1st term)</td>
        <td><asp:Literal runat="server" ID="litPaymentFirstTermDomestic" /></td>
        <asp:PlaceHolder runat="server" ID="plcColIntTotal" Visible="false"><td><asp:Literal runat="server" ID="litPaymentFirstTermInternational" /></td></asp:PlaceHolder>
    </tr>
</table>

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
        <td><asp:Literal runat="server" ID="litCoop" Text="Co-op course tuition ({0}) - this is an academic course and students are required to pay course tuition for one unit."/></td>
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
<table class="tablesaw tablesaw-stack fees-3c" data-tablesaw-mode="stack">
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
            <td><%# ((CostItem)Container.DataItem).Cost.ToString() %></td>
            <asp:PlaceHolder runat="server" ID="plcIntCostItems"><td><%# ((CostItem)Container.DataItem).Cost.ToString() %></td></asp:PlaceHolder>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        <tr class="fees-total-row">
            <td>Total (HST not incuded)</td>
            <td><asp:Literal runat="server" ID="litCostItemsTotalDomestic" /></td>
            <asp:PlaceHolder runat="server" ID="plcIntColCostItemsTotal"><td><asp:Literal runat="server" ID="litCostItemsTotalInternational" /></td></asp:PlaceHolder>
        </tr>
        </table>
    </FooterTemplate>
</asp:Repeater>
<p>Additional program costs generic message</p>
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

<asp:PlaceHolder runat="server" ID="plcNotesPayment" Visible="false">
<div class="fees-notes">
<asp:Literal runat="server" ID="litNotesPayment" />
</div>
</asp:PlaceHolder>



<asp:PlaceHolder runat="server" ID="plcSchedules" Visible="false">
<h2>Schedule and payments</h2>
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
</asp:PlaceHolder>

<asp:PlaceHolder runat="server" ID="plcNotesSchedule" Visible="false">
<div class="fees-notes">
<div><asp:Literal runat="server" ID="litNotesSchedule" /></div>
</div>
</asp:PlaceHolder>

<div>
    <a runat="server" id="lnkAcademicCalendar">View the NSCC Academic Calendar</a>
</div>