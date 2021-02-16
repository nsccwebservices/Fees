<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AtoZ.ascx.cs" Inherits="NSCC.Fees.Web.Controls.AtoZ" %>

<%@ Import Namespace="NSCC.Fees.Data"%>
<%@ Import Namespace="NSCC.Fees.Business.Classes"%>

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
        <h2 class="az-h2" id="<%# ((AlphaCount)Container.DataItem).Alphabet%>"><%# ((AlphaCount)Container.DataItem).Alphabet%></h2>
        <div class="accordion">
        <asp:Repeater runat="server" ID="rptPrograms">
            <ItemTemplate>
                <a href="program.aspx?pfid=<%# ((Program)Container.DataItem).ProgramID%>"><%# ((Program)Container.DataItem).Name%></a>
            </ItemTemplate>
            <SeparatorTemplate><br /></SeparatorTemplate>
        </asp:Repeater>
        </div>
        <a class="top" href="#top">Top</a>
    </ItemTemplate>
</asp:Repeater>
