<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AtoZ.ascx.cs" Inherits="NSCC.Fees.Web.Controls.AtoZ" %>

<%@ Import Namespace="NSCC.Fees.Data"%>
<%@ Import Namespace="NSCC.Fees.Business.Classes"%>

<h2 id="top"><span id="lblTuitionDisplay"><asp:Literal runat="server" ID="litAcademicYear" Text="{0} Program Fees" /></span></h2>

<div class="azBox" id="divAzBox">
    <div>
    <asp:Repeater ID="rptLetters" runat="server" EnableViewState="false">
        <ItemTemplate><a href="<%# String.Concat("#", ((AlphaCount)Container.DataItem).Alphabet)%>"><%# ((AlphaCount)Container.DataItem).Alphabet%></a></ItemTemplate>
        <SeparatorTemplate><span class="alpha_spacer">|</span></SeparatorTemplate>
    </asp:Repeater>
    </div>
</div>

<asp:Repeater runat="server" ID="rptProgramList" OnItemDataBound="rptProgramList_ItemDataBound" EnableViewState="false">
    <ItemTemplate>
        <h3 class="az-h3" id="<%# ((AlphaCount)Container.DataItem).Alphabet%>"><%# ((AlphaCount)Container.DataItem).Alphabet%></h3>
        <asp:Repeater runat="server" ID="rptPrograms">
            <HeaderTemplate><ul class="azWebCal"></HeaderTemplate>
            <ItemTemplate>
                <li><a href="program.aspx?pfid=<%# ((Program)Container.DataItem).ProgramID%>"><%# ((Program)Container.DataItem).Name%></a></li>
            </ItemTemplate>
            <FooterTemplate></ul></FooterTemplate>
        </asp:Repeater>
    </ItemTemplate>
</asp:Repeater>