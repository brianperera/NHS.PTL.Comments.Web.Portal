<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="SpecialtySummary.aspx.cs" Inherits="Nhs.Ptl.Comments.Web.SpecialtySummary" ViewStateMode="Enabled"
    EnableViewState="true" %>

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
            statusSummaryGrid
                    <div class="subSections regularTable">
                        <div class="gridOuter">
                            <asp:GridView ID="statusSummaryGrid" AutoGenerateColumns="True" runat="server" CellPadding="3"
                                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                CssClass="grid" AllowSorting="True" EmptyDataText="No matching records found">
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
