<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Dashboard.aspx.cs" Inherits="Nhs.Ptl.Comments.Web.Dashboard"
    ViewStateMode="Enabled" EnableViewState="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jsapi.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SearchBoxContent" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="main">
        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
                <div class="subSections">
                    <div id="chart_div" style="width: 70%; height: 50%;"></div>
                </div>                
                <div class="subSections">
                    <ul class="formSection noBottonBorders noPadding clearNoMargin">
                                <li><span class="shortFormTitleFieldsWithoutFloat">RTT Wait</span>
                                    <span class="formFieldControl">
                                        <asp:DropDownList ID="RTTWaitDropDown" runat="server" CssClass="defaultDropDown"
                                            AutoPostBack="True" OnSelectedIndexChanged="RTTWaitDropDown_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </span></li>
                            </ul>
                </div>
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
                </div>
            <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
</asp:Content>
