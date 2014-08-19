<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="SpecialtySummary.aspx.cs" Inherits="Nhs.Ptl.Comments.Web.SpecialtySummary"
    ViewStateMode="Enabled" EnableViewState="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../Scripts/ui.core.js" type="text/javascript"></script>
    <script src="../Scripts/effects.core.js" type="text/javascript"></script>
    <script src="../Scripts/ui.dialog.js" type="text/javascript"></script>
    <script src="../Scripts/CommentsScript.js" type="text/javascript"></script>
    <link href="../Styles/themes/base/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/ClientSideUtility.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SearchBoxContent" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="main">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="subSections regularTable">
                    <%--<div class="gridOuter">--%>
                    <asp:GridView ID="statusSummaryGrid" AutoGenerateColumns="True" Width="750px" runat="server"
                        CellPadding="3" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                        CssClass="grid" EmptyDataText="No matching records found" 
                        onrowdatabound="statusSummaryGrid_RowDataBound">
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#BDBDBD" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" CssClass="gridPagerStyle" />
                        <RowStyle ForeColor="#000066" />
                        <AlternatingRowStyle CssClass="altrow" />
                    </asp:GridView>
                    <%--</div>--%>
                </div>
                <div class="subSections">
                    <asp:BarChart ID="statusSummaryBarChart" ChartType="Column" runat="server" Height="200px"
                        ChartTitleColor="#5882FA" CategoryAxisLineColor="#5882FA" ValueAxisLineColor="#5882FA"
                        AutoTextWrap="true" BorderColor="White" BorderStyle="None" 
                        BorderWidth="2px">
                        <Series>
                            <asp:BarChartSeries BarColor="#417696" Name="Total" />
                        </Series>
                    </asp:BarChart>
                </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
